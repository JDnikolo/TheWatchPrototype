using System;
using Input;
using Runtime;
using Runtime.FrameUpdate;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Managers.Persistent
{
	[AddComponentMenu("Managers/Persistent/Input Manager")]
	public sealed class InputManager : Singleton<InputManager>, IFrameUpdatable
	{
		[SerializeField] private InputActionAsset actionAsset;
		[SerializeField] private string playerMapName = "Player";
		[SerializeField] private string uiMapName = "UI";
		[SerializeField] private string persistentGameName = "PersistentGame";
		
		private InputActionMap m_playerMap;
		private InputActionMap m_uiMap;
		private InputActionMap m_persistentGameMap;
		private ControlScheme m_controlScheme;
		private Updatable m_updatable;
		
		protected override bool Override => false;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.InputManager;
		
		private InputActionMap PlayerMap => m_playerMap ??= actionAsset.FindActionMap(playerMapName);

		private InputActionMap UIMap => m_uiMap ??= actionAsset.FindActionMap(uiMapName);
		
		private InputActionMap PersistentGameMap => m_persistentGameMap ??= actionAsset.FindActionMap(persistentGameName);
		
		public static Vector2 MousePosition => Mouse.current.position.ReadValue();
		
		public ControlScheme ControlScheme
		{
			get => m_controlScheme;
			set
			{
				switch (m_controlScheme = value)
				{
					case ControlScheme.Keyboard:
						actionAsset.bindingMask = InputBinding.MaskByGroup("Keyboard&Mouse");
						break;
					case ControlScheme.Gamepad:
						actionAsset.bindingMask = InputBinding.MaskByGroup("Gamepad");
						break;
					case ControlScheme.Touch:
						actionAsset.bindingMask = InputBinding.MaskByGroup("Touch");
						break;
					case ControlScheme.Joystick:
						actionAsset.bindingMask = InputBinding.MaskByGroup("Joystick");
						break;
					case ControlScheme.XR:
						actionAsset.bindingMask = InputBinding.MaskByGroup("XR");
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
		
		public bool RequiresPlayerMap
		{
			get => PlayerMap.enabled;
			set
			{
				if (PlayerMap.enabled == value) return;
				PlayerMap.SetEnabled(value);
				CheckUpdate();
			}
		}

		public bool RequiresUIMap
		{
			get => UIMap.enabled;
			set
			{
				if (UIMap.enabled == value) return;
				UIMap.SetEnabled(value);
				CheckUpdate();
			}
		}

		public bool RequiresPersistentGameMap
		{
			get => PersistentGameMap.enabled;
			set
			{
				if (PersistentGameMap.enabled == value) return;
				PersistentGameMap.SetEnabled(value);
				CheckUpdate();
			}
		}

		private void CheckUpdate() => m_updatable.SetUpdating(
			PlayerMap.enabled || UIMap.enabled || PersistentGameMap.enabled, this);

		public void Stop() => RequiresPlayerMap = RequiresPlayerMap = false;
		
		public void OnFrameUpdate() => InputSystem.Update();

		public static void ToggleCursor(bool enable)
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
		
		public InputAction GetPlayerAction(string actionName) => PlayerMap.FindAction(actionName);
		
		public InputAction GetUIAction(string actionName) => UIMap.FindAction(actionName);
		
		public InputAction GetPersistentGameAction(string actionName) => PersistentGameMap.FindAction(actionName);

		protected override void Awake()
		{
			base.Awake();
			PlayerMap.Disable();
			UIMap.Disable();
			PersistentGameMap.Disable();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			PlayerMap.Disable();
			m_playerMap = null;
			UIMap.Disable();
			m_uiMap = null;
			PersistentGameMap.Disable();
			m_persistentGameMap = null;
		}
	}
}