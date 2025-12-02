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
		private ControlSchemeEnum m_controlScheme = ControlSchemeEnum.ENUM_LENGTH;
		private Updatable m_updatable;
		private int m_activeControls;
		private bool m_cursorVisible;

		protected override bool Override => false;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.InputManager;

		public InputBinding BindingMask
		{
			get
			{
				var possibleMask = actionAsset.bindingMask;
				if (!possibleMask.HasValue) throw new Exception("Load settings first!");
				return possibleMask.Value;
			}
		}

		public ControlSchemeEnum ControlScheme
		{
			get => m_controlScheme;
			set
			{
				if (!Enum.IsDefined(typeof(ControlSchemeEnum),value) || value == ControlSchemeEnum.ENUM_LENGTH) 
					value = ControlSchemeEnum.Keyboard;
				switch (m_controlScheme = value)
				{
					case ControlSchemeEnum.Keyboard:
						actionAsset.bindingMask = InputBinding.MaskByGroup("Keyboard&Mouse");
						break;
					case ControlSchemeEnum.Gamepad:
						actionAsset.bindingMask = InputBinding.MaskByGroup("Gamepad");
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		public string BindingOverridesJson
		{
			get => actionAsset.SaveBindingOverridesAsJson();
			set => actionAsset.LoadBindingOverridesFromJson(value);
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

		public static Vector2 PointerPosition => Pointer.current.position.ReadValue();

		public static bool WasPointerReleasedThisFrame => Pointer.current.press.wasReleasedThisFrame;

		public InputAction GetAction(ControlEnum control)
		{
			switch (control)
			{
				case ControlEnum.KeyboardMoveForward:
				case ControlEnum.KeyboardMoveBackward:
				case ControlEnum.KeyboardMoveLeft:
				case ControlEnum.KeyboardMoveRight:
					return PlayerMap.GetAction("Move");
				case ControlEnum.KeyboardInteract:
					return PlayerMap.GetAction("Interact");
				case ControlEnum.KeyboardShout:
					return PlayerMap.GetAction("Shout");
				case ControlEnum.KeyboardJournal:
					return PersistentGameMap.GetAction("Journal");
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		public int GetBindingIndex(ControlEnum control, ref InputAction action, bool secondary = false)
		{
			action ??= GetAction(control);
			var bindingIndex = action.GetBindingIndex(BindingMask);
			if (secondary) bindingIndex += 1;
			switch (control)
			{
				case ControlEnum.KeyboardMoveForward:
				case ControlEnum.KeyboardInteract:
				case ControlEnum.KeyboardShout:
				case ControlEnum.KeyboardJournal:
					return bindingIndex;
				case ControlEnum.KeyboardMoveBackward:
					return bindingIndex + 2;
				case ControlEnum.KeyboardMoveLeft:
					return bindingIndex + 4;
				case ControlEnum.KeyboardMoveRight:
					return bindingIndex + 6;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public static string GetDisplayString(InputAction action, int bindingIndex) => 
			action.bindings[bindingIndex].ToDisplayString();

		public static bool HasSecondary(ControlEnum control)
		{
			switch (control)
			{
				case ControlEnum.KeyboardMoveForward:
				case ControlEnum.KeyboardMoveBackward:
				case ControlEnum.KeyboardMoveLeft:
				case ControlEnum.KeyboardMoveRight:
				case ControlEnum.KeyboardInteract:
				case ControlEnum.KeyboardShout:
					return true;
				case ControlEnum.KeyboardJournal:
					return false;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
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
			if (m_cursorVisible == enable) return;
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