using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
	public static partial class Utils
	{
		/// <summary>
		/// Tests if a game object is the one assigned to <see cref="PlayerManager.PlayerObject"/>.
		/// </summary>
		public static bool IsPlayerObject(this GameObject gameObject)
		{
			var playerManager = PlayerManager.Instance;
			if (playerManager != null)
			{
				var playerObject = playerManager.PlayerObject;
				if (playerObject) return gameObject == playerObject;
			}

			return false;
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

		public static T GetRandom<T>(this IList<T> list)
		{
			if (list == null) throw new ArgumentNullException(nameof(list));
			var length = list.Count;
			if (length == 0) throw new InvalidOperationException("List is empty");
			if (length == 1) return list[0];
			return list[Random.Range(0, length)];
		}
	}
}