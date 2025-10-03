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

		public Vector2 mousePosition;
		private InputActionMap m_playerMap;
		private InputActionMap m_uiMap;

		public bool PlayerMapEnabled => m_playerMap.enabled;
		public bool UIMapEnabled => m_uiMap.enabled;

		public static Vector2 MousePosition => Mouse.current.position.ReadValue();

		private void Start()
		{
			m_playerMap = actionAsset.FindActionMap(playerMapName);
			m_uiMap = actionAsset.FindActionMap(uiMapName);
		}

		private void Update() => mousePosition = MousePosition;

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
			if (enable == m_playerMap.enabled) return;
			if (enable) m_playerMap.Enable();
			else m_playerMap.Disable();
		}

		public void ToggleUIMap(bool enable)
		{
			if (enable == m_uiMap.enabled) return;
			if (enable) m_uiMap.Enable();
			else m_uiMap.Disable();
		}
		
		public InputAction GetPlayerAction(string name) => m_playerMap.FindAction(name);
		
		public InputAction GetUIAction(string name) => m_uiMap.FindAction(name);
	}
}