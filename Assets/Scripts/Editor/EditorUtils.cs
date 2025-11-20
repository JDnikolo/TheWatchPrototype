using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	public static class EditorUtils
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
	}
}