using System;
using Input;
using Runtime;
using Runtime.FrameUpdate;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers.Persistent
{
	[AddComponentMenu("Managers/Persistent/Input Manager")]
	public sealed partial class InputManager : Singleton<InputManager>, IFrameUpdatable
	{
		[SerializeField] private InputActionAsset actionAsset;
		[SerializeField] private string playerMapName = "Player";
		[SerializeField] private string uiMapName = "UI";
		[SerializeField] private string persistentGameName = "PersistentGame";
		
		private InputMap m_playerMap = new(ControlMap.Player);
		private InputMap m_uiMap = new(ControlMap.UI);
		private InputMap m_persistentGameMap = new(ControlMap.PersistentGame);
		private ControlScheme m_controlScheme;
		private Updatable m_updatable;
		private int m_activeControls;
		private bool m_cursorVisible;

		protected override bool Override => false;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.InputManager;
		
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

		public State PauseState
		{
			get => new()
			{
				ActiveControls = m_activeControls,
				CursorVisible = m_cursorVisible,
			};
			set
			{
				m_activeControls = value.ActiveControls;
				SetFromFlag(m_playerMap);
				SetFromFlag(m_uiMap);
				SetFromFlag(m_persistentGameMap);
				ToggleCursor(value.CursorVisible);
				CheckUpdate();
			}
		}
		
		public InputMap PlayerMap => m_playerMap;
		
		public InputMap UIMap => m_uiMap;
		
		public InputMap PersistentGameMap => m_persistentGameMap;
		
		public void Stop()
		{
			m_playerMap.ForceDisable();
			m_uiMap.ForceDisable();
			m_persistentGameMap.ForceDisable();
			m_activeControls = 0;
			CheckUpdate();
		}

		public void OnFrameUpdate() => InputSystem.Update();
		
		public void ToggleCursor(bool enable)
		{
			m_cursorVisible = enable;
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

		private void CheckUpdate() => m_updatable.SetUpdating(m_activeControls != 0, this);

		private void SetFlag(int mask, bool value)
		{
			if (value) m_activeControls |= mask;
			else m_activeControls &= ~mask;
			CheckUpdate();
		}

		private void SetFromFlag(InputMap map)
		{
			if ((map.BitFlag & m_activeControls) != 0) map.ForceEnable();
			else map.ForceDisable();
		}
		
		private InputActionMap GetMapByControl(ControlMap map)
		{
			switch (map)
			{
				case ControlMap.Player:
					return actionAsset.FindActionMap(playerMapName);
				case ControlMap.UI:
					return actionAsset.FindActionMap(uiMapName);
				case ControlMap.PersistentGame:
					return actionAsset.FindActionMap(persistentGameName);
				default:
					throw new ArgumentOutOfRangeException(nameof(map), map, null);
			}
		}
		
		protected override void Awake()
		{
			base.Awake();
			Stop();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			m_playerMap.Dispose();
			m_playerMap = null;
			m_uiMap.Dispose();
			m_uiMap = null;
			m_persistentGameMap.Dispose();
			m_persistentGameMap = null;
		}
	}
}