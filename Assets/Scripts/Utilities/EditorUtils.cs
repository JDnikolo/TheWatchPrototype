#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Runtime.Automation;
using UnityEditor;
using UnityEngine;

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

		/// <summary>
		/// Displays an object field if object, or the type name/null as label.
		/// </summary>
		public static void DisplayObject<T>(this T value, string name, bool displayNull = true)
		{
			if (value == null)
			{
				if (displayNull)
				{
					const string nullStr = "Null";
					if (string.IsNullOrEmpty(name)) EditorGUILayout.TextField(nullStr);
					EditorGUILayout.TextField(name, nullStr);
				}

				return;
			}

			if (value is UnityEngine.Object obj)
			{
				var type = value.GetType();
				if (string.IsNullOrEmpty(name)) EditorGUILayout.ObjectField(obj, type, false);
				else EditorGUILayout.ObjectField(name, obj, type, false);
			}
			else if (value is IEditorDisplayable displayable) displayable.DisplayInEditor(name);
			else
			{
				var type = value.GetType();
				if (type.IsPrimitive)
				{
					throw new NotImplementedException();
				}
				else
				{
					var typeStr = type.ToString();
					if (string.IsNullOrEmpty(name)) EditorGUILayout.TextField(typeStr);
					EditorGUILayout.TextField(name, typeStr);
				}
			}
		}

		public static void DisplayObjectCollection<T>(this ICollection<T> value, string name, bool displayNull = true)
		{
			if (value == null)
			{
				if (displayNull)
				{
					const string nullStr = "Null";
					if (string.IsNullOrEmpty(name)) EditorGUILayout.TextField(nullStr);
					EditorGUILayout.TextField(name, nullStr);
				}

				return;
			}
			
			if (!string.IsNullOrEmpty(name)) EditorGUILayout.LabelField(name);
			if (value.Count < 1) return;
			EditorGUI.indentLevel += 1;
			foreach (var element in value) element.DisplayObject(null);
			EditorGUI.indentLevel -= 1;
		}
		
		public static void DisplayObjectDictionary<TKey, TValue>(this IDictionary<TKey, TValue> value, 
			string name, bool displayNull = true)
		{
			if (value == null)
			{
				if (displayNull)
				{
					const string nullStr = "Null";
					if (string.IsNullOrEmpty(name)) EditorGUILayout.TextField(nullStr);
					EditorGUILayout.TextField(name, nullStr);
				}

				return;
			}
			
			if (!string.IsNullOrEmpty(name)) EditorGUILayout.LabelField(name);
			if (value.Count < 1) return;
			EditorGUI.indentLevel += 1;
			foreach (var pair in value)
			{
				EditorGUILayout.BeginHorizontal();
				pair.Key.DisplayObject(null);
				pair.Value.DisplayObject(null);
				EditorGUILayout.EndHorizontal();
			}
			
			EditorGUI.indentLevel -= 1;
		}

		public static void DisplayInEditor<T>(this T value, string name) where T : IEditorDisplayable
		{
			if (string.IsNullOrEmpty(name)) value.DisplayInEditor();
			else
			{
				EditorGUILayout.LabelField(name);
				EditorGUI.indentLevel++;
				value.DisplayInEditor();
				EditorGUI.indentLevel--;
			}

		}
	}
}
#endif