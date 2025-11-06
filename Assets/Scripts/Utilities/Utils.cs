using Managers;
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
	}
}