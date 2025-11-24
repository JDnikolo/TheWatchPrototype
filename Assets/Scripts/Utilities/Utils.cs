using Managers;
using Managers.Persistent;
using UnityEngine;
using UnityEngine.InputSystem;

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
			inputManager.PlayerMap.Enabled = true;
			inputManager.UIMap.Enabled = false;
			inputManager.ToggleCursor(false);
		}

		/// <summary>
		/// Ensures only the UI input is usable.
		/// </summary>
		/// <remarks>Enables cursor.</remarks>
		public static void ForceUIInput(this InputManager inputManager)
		{
			inputManager.UIMap.Enabled = true;
			inputManager.PlayerMap.Enabled = false;
			inputManager.ToggleCursor(true);
		}

		public static void SetEnabled(this InputActionMap map, bool enabled)
		{
			if (enabled) map.Enable();
			else map.Disable();
		}
	}
}