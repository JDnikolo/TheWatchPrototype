#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using Attributes;
using AYellowpaper.SerializedCollections;
using Debugging;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Utilities
{
	public static partial class Utils
	{
		public static bool IsAssetPath(this string path) => path.StartsWith("Assets");

		public static bool TestAny(this OperationData operationData, string path, object value)
		{
			if (value == null) throw new InvalidOperationException();
			var fields = new List<FieldInfo>();
			var type = value.GetType();
			fields.AddRange(GetFields(type));
			var baseType = type.BaseType;
			while (baseType != null && baseType != typeof(object) && 
					baseType != typeof(BaseBehaviour) && baseType != typeof(BaseObject))
			{
				fields.AddRange(GetFields(baseType));
				baseType = baseType.BaseType;
			}

			for (var i = 0; i < fields.Count; i++)
			{
				var field = fields[i];
				var isSerialized = false;
				var allowNull = false;
				var obsolete = false;
				var targetCount = 0;
				string debugMethod = null;
				foreach (var attribute in field.GetCustomAttributes())
				{
					if (attribute is SerializeField) isSerialized = true;
					else if (attribute is ObsoleteAttribute) obsolete = true;
					else if (attribute is CanBeNull) allowNull = true;
					else if (attribute is CanBeNullInPath pathTester) allowNull = pathTester.AllowNull(path);
					else if (attribute is MinCount minCount) targetCount = minCount.Target;
					else if (attribute is CustomDebug customDebug) debugMethod = customDebug.MethodName;
				}
				
				if (!isSerialized) continue;
				var fieldData = new FieldData(obsolete, allowNull, targetCount, debugMethod);
				var fieldType = field.FieldType;
				var typeData = fieldType.GetTypeData(false);
				if (typeData == TypeData.NotTestable) continue;
				path = $"{path}.{field.Name}";
				if (!string.IsNullOrEmpty(fieldData.CustomDebug))
				{
					var method = type.GetMethod(fieldData.CustomDebug,
						BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly,
						null, CallingConventions.HasThis,
						new[] {typeof(OperationData), typeof(string), typeof(FieldData)}, null);
					if (method != null)
						return (bool) method.Invoke(value, new object[] {operationData, path, fieldData});
				}

				//if (!operationData.TestType(path, fieldData, typeData, fieldType, field.GetValue(value))) return false;
				operationData.TestType(path, fieldData, typeData, fieldType, field.GetValue(value));
			}

			return true;
		}

		private static FieldInfo[] GetFields(Type type) =>
			type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

		private static bool TestType(this OperationData operationData, string path,
			FieldData fieldData, TypeData typeData, Type type, object value)
		{
			switch (typeData)
			{
				case TypeData.UnityObject:
					return operationData.TestUnityObject(path, fieldData, value as Object);
				case TypeData.Array:
					return operationData.TestArray(path, fieldData, type.GetElementType(), value);
				case TypeData.List:
					return operationData.TestList(path, fieldData,
						type.GenericTypeArguments[0], value);
				case TypeData.Dictionary:
					return operationData.TestDictionary(path, fieldData,
						type.GenericTypeArguments[0], type.GenericTypeArguments[1], value);
				case TypeData.Serialized:
					return operationData.TestSerialized(path, fieldData, value);
			}

			return true;
		}

		public static bool TestObject(this OperationData operationData, string path, FieldData fieldData, object value)
		{
			if (fieldData.Obsolete)
			{
				operationData.LogMessage(path, "Obsolete");
				return false;
			}
			
			if (value == null)
			{
				if (fieldData.AllowNull) return true;
				operationData.LogMessage(path, NULL_STRING);
				return false;
			}

			return true;
		}

		public static bool TestUnityObject(this OperationData operationData, string path,
			FieldData fieldData, Object value)
		{
			if (fieldData.Obsolete)
			{
				operationData.LogMessage(path, "Obsolete");
				return false;
			}
			
			if (!value)
			{
				if (fieldData.AllowNull) return true;
				operationData.LogMessage(path, NULL_STRING);
				return false;
			}

			return true;
		}

		public static bool TestElement<T>(this OperationData operationData, string path,
			FieldData fieldData, T element, bool native, Type elementType = null, TypeData? typeData = null)
		{
			if (!operationData.TestObject(path, fieldData, element)) return false;
			elementType ??= typeof(T);
			typeData ??= elementType.GetTypeData(native);
			return operationData.TestType(path, fieldData, typeData.Value, elementType, element);
		}

		public static bool TestCollectionCount<T>(this OperationData operationData, string path,
			FieldData fieldData, ICollection<T> collection)
		{
			if (collection.Count < fieldData.MinCount)
			{
				operationData.LogMessage(path, "Collection is too small");
				return false;
			}

			return true;
		}

		public static bool TestIListElements<T>(this OperationData operationData, string path,
			FieldData fieldData, IList<T> list)
		{
			const bool native = true;
			var elementType = typeof(T);
			var typeData = elementType.GetTypeData(native);
			for (var i = 0; i < list.Count; i++)
			{
				var localPath = $"{path}[{i}]";
				var element = list[i];
				if (!operationData.TestElement(localPath, fieldData, element, native, elementType, typeData))
					return false;
			}

			return true;
		}

		public static bool TestArray(this OperationData operationData, string path,
			FieldData fieldData, Type elementType, object value)
		{
			if (elementType.GetTypeData(true) != TypeData.NotTestable)
			{
				var arrayMethods = operationData.ArrayMethods;
				var save = arrayMethods != null;
				if (!save || !arrayMethods.TryGetValue(elementType, out var method))
				{
					method = GetGenericMethod(nameof(TestArrayGeneric));
					if (method == null)
						throw new InvalidOperationException($"Unable to make array method for {elementType}");
					method = method.MakeGenericMethod(elementType);
					if (save) arrayMethods.Add(elementType, method);
				}

				return (bool) method.Invoke(null, new[] {operationData, path, fieldData, value});
			}

			return true;
		}

		public static bool TestArrayGeneric<T>(this OperationData operationData, string path,
			FieldData fieldData, T[] array) =>
			operationData.TestObject(path, fieldData, array) &&
			operationData.TestCollectionCount(path, fieldData, array) &&
			operationData.TestIListElements(path, fieldData, array);

		public static bool TestList(this OperationData operationData, string path,
			FieldData fieldData, Type elementType, object value)
		{
			if (elementType.GetTypeData(false) != TypeData.NotTestable)
			{
				var listMethods = operationData.ListMethods;
				var save = listMethods != null;
				if (!save || !listMethods.TryGetValue(elementType, out var method))
				{
					method = GetGenericMethod(nameof(TestListGeneric));
					if (method == null)
						throw new InvalidOperationException($"Unable to make list method for {elementType}");
					method = method.MakeGenericMethod(elementType);
					if (save) listMethods.Add(elementType, method);
				}

				return (bool) method.Invoke(null, new[] {operationData, path, fieldData, value});
			}

			return true;
		}

		public static bool TestListGeneric<T>(this OperationData operationData, string path,
			FieldData fieldData, List<T> list) =>
			operationData.TestObject(path, fieldData, list) &&
			operationData.TestCollectionCount(path, fieldData, list) &&
			operationData.TestIListElements(path, fieldData, list);

		public static bool TestDictionary(this OperationData operationData, string path,
			FieldData fieldData, Type keyType, Type valueType, object value)
		{
			var typeData = keyType.GetTypeData(true);
			if (typeData == TypeData.NotTestable) typeData = valueType.GetTypeData(false);
			if (typeData != TypeData.NotTestable)
			{
				var dictionaryType = new DictionaryType(keyType, valueType);
				var dictionaryMethods = operationData.DictionaryMethods;
				var save = dictionaryMethods != null;
				if (!save || !dictionaryMethods.TryGetValue(dictionaryType, out var method))
				{
					method = GetGenericMethod(nameof(TestDictionaryGeneric));
					if (method == null)
						throw new InvalidOperationException(
							$"Unable to make dictionary method for <{keyType}, {valueType}>");
					method = method.MakeGenericMethod(keyType, valueType);
					if (save) dictionaryMethods.Add(dictionaryType, method);
				}

				if (method != null) return (bool) method.Invoke(null, new[] {operationData, path, fieldData, value});
			}

			return true;
		}

		public static bool TestDictionaryGeneric<TKey, TValue>(this OperationData operationData, string path,
			FieldData fieldData, Dictionary<TKey, TValue> dictionary)
		{
			if (!operationData.TestObject(path, fieldData, dictionary) ||
				!operationData.TestCollectionCount(path, fieldData, dictionary)) return false;
			const bool keyNative = true;
			var keyType = typeof(TKey);
			var keyData = keyType.GetTypeData(keyNative);
			const bool valueNative = false;
			var valueType = typeof(TValue);
			var valueData = valueType.GetTypeData(valueNative);
			foreach (var (key, value) in dictionary)
			{
				string localPath;
				if (keyData != TypeData.NotTestable)
				{
					localPath = $"{path}[Key]";
					if (!operationData.TestElement(localPath, fieldData, key, keyNative, keyType, keyData))
						return false;
				}

				if (valueData != TypeData.NotTestable)
				{
					localPath = $"{path}[Value]";
					if (!operationData.TestElement(localPath, fieldData, value, valueNative, valueType, valueData))
						return false;
				}
			}

			return true;
		}

		public static bool TestSerialized(this OperationData operationData, string path,
			FieldData fieldData, object value) => operationData.TestObject(path,
			fieldData, value) && operationData.TestAny(path, value);

		public static TypeData GetTypeData(this Type type, bool native)
		{
			if (!type.IsPrimitive && type != typeof(string))
			{
				if (type.IsSubclassOf(typeof(Object))) return TypeData.UnityObject;
				if (type.IsArray && !native) return TypeData.Array;
				if (type.IsConstructedGenericType)
				{
					var genericType = type.GetGenericTypeDefinition();
					if (genericType == typeof(List<>) && !native) return TypeData.List;
					if (genericType == typeof(SerializedDictionary<,>)) return TypeData.Dictionary;
				}

				if (type.GetCustomAttribute<SerializableAttribute>() != null) return TypeData.Serialized;
			}

			return TypeData.NotTestable;
		}

		private static MethodInfo GetGenericMethod(string name) => typeof(Utils).GetMethod(name);

		private static void LogMessage(this OperationData data, string path, string message) =>
			Debug.LogWarning($"{path}: {message}", data.Instance);
	}
}
#endif