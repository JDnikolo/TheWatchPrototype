using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
	[AddComponentMenu("Managers/Input Manager")]
	public sealed class InputManager : Singleton<InputManager>
	{
		[SerializeField] private InputActionAsset actionAsset;
		[SerializeField] private string playerMapName = "Player";
		[SerializeField] private string uiMapName = "UI";
		
		private InputActionMap m_playerMap;
		private InputActionMap m_uiMap;

		protected override bool Override => false;
		
		private InputActionMap PlayerMap => m_playerMap ??= actionAsset.FindActionMap(playerMapName);

		private InputActionMap UIMap => m_uiMap ??= actionAsset.FindActionMap(uiMapName);

		public bool PlayerMapEnabled => PlayerMap.enabled;
		
		public bool UIMapEnabled => UIMap.enabled;
		
		public static Vector2 MousePosition => Mouse.current.position.ReadValue();

		public void ToggleCursor(bool enable)
		{
			if (enable)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			else
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}

		public void TogglePlayerMap(bool enable)
		{
			if (enable == PlayerMap.enabled) return;
			if (enable) PlayerMap.Enable();
			else PlayerMap.Disable();
		}

		public void ToggleUIMap(bool enable)
		{
			if (enable == UIMap.enabled) return;
			if (enable) UIMap.Enable();
			else UIMap.Disable();
		}

		public InputActionMap GetInputMap(string mapName)
		{
			if (mapName == playerMapName) return PlayerMap;
			if (mapName == uiMapName) return UIMap;
			return actionAsset.FindActionMap(mapName);
		}
		
		public InputAction GetPlayerAction(string actionName) => PlayerMap.FindAction(actionName);
		
		public InputAction GetUIAction(string actionName) => UIMap.FindAction(actionName);
	}
}