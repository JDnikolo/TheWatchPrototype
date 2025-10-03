using System;
using System.Collections.Generic;
using Managers;
using UnityEditor;
using UnityEngine;

namespace Utilities
{
	public static partial class Utils
	{
		/// <summary>
		/// Tests if a game object is the one assigned to <see cref="PlayerManager.PlayerObject"/>.
		/// </summary>
		public static bool IsPlayerObject(this GameObject gameObject)
		{
			var playerObject = PlayerManager.Instance.PlayerObject;
			if (!playerObject) return false;
			return gameObject == playerObject;
		}

		/// <summary>
		/// Ensures only the Player input is usable.
		/// </summary>
		/// <remarks>Disables cursor.</remarks>
		public static void ForcePlayerInput(this InputManager inputManager)
		{
			inputManager.ToggleUIMap(false);
			inputManager.TogglePlayerMap(true);
			inputManager.ToggleCursor(false);
		}

		/// <summary>
		/// Ensures only the UI input is usable.
		/// </summary>
		/// <remarks>Enables cursor.</remarks>
		public static void ForceUIInput(this InputManager inputManager)
		{
			inputManager.TogglePlayerMap(false);
			inputManager.ToggleUIMap(true);
			inputManager.ToggleCursor(true);
		}

		/// <summary>
		/// Gets every game object in the scene.
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
	}
}