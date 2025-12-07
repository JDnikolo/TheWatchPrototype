#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Runtime.Automation;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utilities
{
	public static partial class Utils
	{
		/// <summary>
		/// Gets every <see cref="GameObject"/> in the scene.
		/// </summary>
		/// <remarks>Expensive!</remarks>
		public static void GetAllObjectsInScene(this List<GameObject> objectsInScene)
		{
			if (objectsInScene == null) throw new ArgumentNullException(nameof(objectsInScene));
			foreach (var gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
				if (gameObject.hideFlags == HideFlags.None &&
					(PrefabUtility.GetPrefabAssetType(gameObject) == PrefabAssetType.NotAPrefab ||
					PrefabUtility.GetPrefabInstanceStatus(gameObject) == PrefabInstanceStatus.Connected))
					objectsInScene.Add(gameObject);
		}

		/// <summary>
		/// Gets every <see cref="MonoBehaviour"/> in the scene.
		/// </summary>
		/// <remarks>Expensive!</remarks>
		public static void GetAllBehavioursInScene(this List<MonoBehaviour> behaviorsInScene)
		{
			if (behaviorsInScene == null) throw new ArgumentNullException(nameof(behaviorsInScene));
			foreach (var monoBehavior in Resources.FindObjectsOfTypeAll<MonoBehaviour>())
			{
				var gameObject = monoBehavior.gameObject;
				if (gameObject.hideFlags == HideFlags.None &&
					(PrefabUtility.GetPrefabAssetType(gameObject) == PrefabAssetType.NotAPrefab ||
					PrefabUtility.GetPrefabInstanceStatus(gameObject) == PrefabInstanceStatus.Connected))
					behaviorsInScene.Add(monoBehavior);
			}
		}

		private static void DisplayNullObject(string name)
		{
			if (string.IsNullOrEmpty(name)) EditorGUILayout.ObjectField(null, typeof(Component), false);
			EditorGUILayout.ObjectField(name, null, typeof(Component), false);
		}

		/// <summary>
		/// Displays an object field if object, or the type name/null as label.
		/// </summary>
		public static void Display<T>(this T value, string name)
		{
			if (value == null)
			{
				DisplayNullObject(name);
				return;
			}

			if (value is IEditorDisplayable displayable)
			{
				if (string.IsNullOrEmpty(name)) displayable.DisplayInEditor();
				else displayable.DisplayInEditor(name);
			}
			else if (value is Object obj)
			{
				var type = value.GetType();
				if (string.IsNullOrEmpty(name)) EditorGUILayout.ObjectField(obj, type, false);
				else EditorGUILayout.ObjectField(name, obj, type, false);
			}
			else
			{
				var type = value.GetType();
				if (type.IsPrimitive)
				{
					if (value is bool boolVal)
					{
						if (string.IsNullOrEmpty(name)) EditorGUILayout.Toggle(boolVal);
						else EditorGUILayout.Toggle(name, boolVal);
					}
					else if (value is int intVal)
					{
						if (string.IsNullOrEmpty(name)) EditorGUILayout.IntField(intVal);
						else EditorGUILayout.IntField(name, intVal);
					}
					else if (value is long longVal)
					{
						if (string.IsNullOrEmpty(name)) EditorGUILayout.LongField(longVal);
						else EditorGUILayout.LongField(name, longVal);
					}
					else if (value is float floatVal)
					{
						if (string.IsNullOrEmpty(name)) EditorGUILayout.FloatField(floatVal);
						else EditorGUILayout.FloatField(name, floatVal);
					}
					else if (value is double doubleVal)
					{
						if (string.IsNullOrEmpty(name)) EditorGUILayout.DoubleField(doubleVal);
						else EditorGUILayout.DoubleField(name, doubleVal);
					}
					else if (value is char charVal)
					{
						var strVal = charVal.ToString();
						if (string.IsNullOrEmpty(name)) EditorGUILayout.TextField(strVal);
						else EditorGUILayout.TextField(name, strVal);
					}
				}
				else if (value is Enum enumVal)
				{
					if (string.IsNullOrEmpty(name)) EditorGUILayout.EnumPopup(enumVal);
					else EditorGUILayout.EnumPopup(name, enumVal);
				}
				else if (value is string strVal)
				{
					if (string.IsNullOrEmpty(name)) EditorGUILayout.TextField(strVal);
					else EditorGUILayout.TextField(name, strVal);
				}
				else
				{
					var typeStr = type.ToString();
					if (string.IsNullOrEmpty(name)) EditorGUILayout.TextField(typeStr);
					else EditorGUILayout.TextField(name, typeStr);
				}
			}
		}

		public static void DisplayCollection<T>(this ICollection<T> value,
			string name, bool displayNull = true, Func<T, bool> customFilter = null)
		{
			if (value == null)
			{
				if (displayNull) DisplayNullObject(name);
				return;
			}

			if (!string.IsNullOrEmpty(name)) EditorGUILayout.LabelField(name);
			if (value.Count < 1) return;
			EditorGUI.indentLevel += 1;
			foreach (var element in value)
				if (customFilter == null || !customFilter.Invoke(element))
					element.Display(null);
			EditorGUI.indentLevel -= 1;
		}

		public static void DisplayReadOnlyCollection<T>(this IReadOnlyCollection<T> value,
			string name, bool displayNull = true, Func<T, bool> customFilter = null)
		{
			if (value == null)
			{
				if (displayNull) DisplayNullObject(name);
				return;
			}

			if (!string.IsNullOrEmpty(name)) EditorGUILayout.LabelField(name);
			if (value.Count < 1) return;
			EditorGUI.indentLevel += 1;
			foreach (var element in value)
				if (customFilter == null || !customFilter.Invoke(element))
					element.Display(null);
			EditorGUI.indentLevel -= 1;
		}

		public static void DisplayDictionary<TKey, TValue>(this IDictionary<TKey, TValue> value,
			string name, bool displayNull = true, Func<TKey, TValue, bool> customFilter = null)
		{
			if (value == null)
			{
				if (displayNull) DisplayNullObject(name);
				return;
			}

			if (!string.IsNullOrEmpty(name)) EditorGUILayout.LabelField(name);
			if (value.Count < 1) return;
			EditorGUI.indentLevel += 1;
			foreach (var (key, val) in value)
			{
				EditorGUILayout.BeginHorizontal();
				if (customFilter == null || !customFilter.Invoke(key, val))
				{
					key.Display(null);
					val.Display(null);
				}

				EditorGUILayout.EndHorizontal();
			}

			EditorGUI.indentLevel -= 1;
		}

		public static void DisplayReadOnlyDictionary<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> value,
			string name, bool displayNull = true, Func<TKey, TValue, bool> customFilter = null)
		{
			if (value == null)
			{
				if (displayNull) DisplayNullObject(name);
				return;
			}

			if (!string.IsNullOrEmpty(name)) EditorGUILayout.LabelField(name);
			if (value.Count < 1) return;
			EditorGUI.indentLevel += 1;
			foreach (var (key, val) in value)
			{
				EditorGUILayout.BeginHorizontal();
				if (customFilter == null || !customFilter.Invoke(key, val))
				{
					key.Display(null);
					val.Display(null);
				}

				EditorGUILayout.EndHorizontal();
			}

			EditorGUI.indentLevel -= 1;
		}

		public static void DisplayIndented(this string name, Action action)
		{
			if (string.IsNullOrEmpty(name))
			{
				action?.Invoke();
				return;
			}

			EditorGUILayout.LabelField(name);
			EditorGUI.indentLevel++;
			action?.Invoke();
			EditorGUI.indentLevel--;
		}

		public static void DirtyReplaceObject<T>(this Object target, ref T value, T newValue) where T : Object
		{
			if (newValue == value) return;
			Debug.Log($"[{target.GetType()}] Replacing {value.ToStringReference()} with {newValue.ToStringReference()}",
				target);
			value = newValue;
			if (target) EditorUtility.SetDirty(target);
		}

		public static void DirtyReplaceReference<T>(this Object target, ref T value, T newValue,
			IEqualityComparer<T> comparer = null) where T : class
		{
			comparer ??= EqualityComparer<T>.Default;
			if (comparer.Equals(value, newValue)) return;
			Debug.Log($"[{target.GetType()}] Replacing {value.ToStringReference()} with {newValue.ToStringReference()}",
				target);
			value = newValue;
			if (target) EditorUtility.SetDirty(target);
		}

		public static void DirtyReplaceValue<T>(this Object target, ref T value, T newValue,
			IEqualityComparer<T> comparer = null) where T : struct
		{
			comparer ??= EqualityComparer<T>.Default;
			if (comparer.Equals(value, newValue)) return;
			Debug.Log($"[{target.GetType()}] Replacing {value} with {newValue}", target);
			value = newValue;
			if (target) EditorUtility.SetDirty(target);
		}

		public static void DirtyReplaceNullable<T>(this Object target, ref T? value, T? newValue,
			IEqualityComparer<T?> comparer = null) where T : struct
		{
			comparer ??= EqualityComparer<T?>.Default;
			if (comparer.Equals(value, newValue)) return;
			Debug.Log($"[{target.GetType()}] Replacing {value.ToStringNullable()} with {newValue.ToStringNullable()}",
				target);
			value = newValue;
			if (target) EditorUtility.SetDirty(target);
		}

		public static void UpdateNameTo(this Component instance, Object target)
		{
			if (!target || EditorSceneManager.IsPreviewSceneObject(instance)) return;
			UpdateNameTo(instance, target.name);
		}

		public static void UpdateNameTo(this Component instance, string name)
		{
			if (EditorSceneManager.IsPreviewSceneObject(instance)) return;
			var gameObject = instance.gameObject;
			if (gameObject.name == name) return;
			gameObject.name = name;
			EditorUtility.SetDirty(gameObject);
		}
	}
}
#endif