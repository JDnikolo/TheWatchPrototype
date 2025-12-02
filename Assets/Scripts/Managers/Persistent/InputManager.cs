using System;
using Exceptions;
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
		private bool m_cursorVisible = true;

		private const string KeyboardAndMouse = "Keyboard&Mouse";
		private const string Gamepad = "Gamepad";
		
		public static InputBinding KeyboardMask => InputBinding.MaskByGroup(KeyboardAndMouse);
		
		public static InputBinding GamepadMask => InputBinding.MaskByGroup(Gamepad);
		
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
			get
			{
				if (m_controlScheme >= ControlSchemeEnum.ENUM_LENGTH) throw new LoadFirstException();
				return m_controlScheme;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ControlSchemeEnum),value) || value == ControlSchemeEnum.ENUM_LENGTH) 
					value = ControlSchemeEnum.Keyboard;
				switch (m_controlScheme = value)
				{
					case ControlSchemeEnum.Keyboard:
						actionAsset.bindingMask = KeyboardMask;
						break;
					case ControlSchemeEnum.Gamepad:
						actionAsset.bindingMask = GamepadMask;
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

		public InputAction GetAction(GroupedControlEnum control)
		{
			switch (control)
			{
				case GroupedControlEnum.MoveForward:
				case GroupedControlEnum.MoveBackward:
				case GroupedControlEnum.MoveLeft:
				case GroupedControlEnum.MoveRight:
					return PlayerMap.GetAction("Move");
				case GroupedControlEnum.Interact:
					return PlayerMap.GetAction("Interact");
				case GroupedControlEnum.Shout:
					return PlayerMap.GetAction("Shout");
				case GroupedControlEnum.Journal:
					return PersistentGameMap.GetAction("Journal");
				case GroupedControlEnum.Help:
					return PersistentGameMap.GetAction("Help");
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		public int GetBindingIndex(FullControlEnum control, ref InputAction action, bool secondary = false)
		{
			action ??= GetAction(GetGroupedControl(control));
			InputBinding bindingMask;
			switch (GetControlScheme(control))
			{
				case ControlSchemeEnum.Keyboard:
					bindingMask = KeyboardMask;
					break;
				case ControlSchemeEnum.Gamepad:
					bindingMask = GamepadMask;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			
			return GetBindingIndex(control, action.GetBindingIndex(bindingMask), secondary);
		}
		
		private int GetBindingIndex(FullControlEnum control, int bindingIndex, bool secondary)
		{
			if (secondary) bindingIndex += 1;
			switch (control)
			{
				case FullControlEnum.KeyboardMoveForward:
				case FullControlEnum.KeyboardInteract:
				case FullControlEnum.KeyboardShout:
				case FullControlEnum.KeyboardJournal:
				case FullControlEnum.KeyboardHelp:
					return bindingIndex;
				case FullControlEnum.KeyboardMoveBackward:
					return bindingIndex + 2;
				case FullControlEnum.KeyboardMoveLeft:
					return bindingIndex + 4;
				case FullControlEnum.KeyboardMoveRight:
					return bindingIndex + 6;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public (int, int) GetBindingIndex(GroupedControlEnum control, ref InputAction action)
		{
			action ??= GetAction(control);
			var bindingIndex = action.GetBindingIndex(BindingMask);
			var fullControl = GetFullControl(control);
			var hasSecondary = HasSecondary(fullControl);
			var firstIndex = GetBindingIndex(fullControl, bindingIndex, false);
			if (hasSecondary)
			{
				return (firstIndex, GetBindingIndex(fullControl, bindingIndex, true));
			}

			return (firstIndex, -1);
		}

		public FullControlEnum GetFullControl(GroupedControlEnum control)
		{
			ControlSchemeEnum controlScheme;
			switch (BindingMask.groups)
			{
				case KeyboardAndMouse:
					controlScheme = ControlSchemeEnum.Keyboard;
					break;
				case Gamepad:
					controlScheme = ControlSchemeEnum.Gamepad;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			switch (control)
			{
				case GroupedControlEnum.MoveForward:
					switch (controlScheme)
					{
						case ControlSchemeEnum.Keyboard:
							return FullControlEnum.KeyboardMoveForward;
						default:
							throw new ArgumentOutOfRangeException();
					}
				case GroupedControlEnum.MoveBackward:
					switch (controlScheme)
					{
						case ControlSchemeEnum.Keyboard:
							return FullControlEnum.KeyboardMoveBackward;
						default:
							throw new ArgumentOutOfRangeException();
					}
				case GroupedControlEnum.MoveLeft:
					switch (controlScheme)
					{
						case ControlSchemeEnum.Keyboard:
							return FullControlEnum.KeyboardMoveLeft;
						default:
							throw new ArgumentOutOfRangeException();
					}
				case GroupedControlEnum.MoveRight:
					switch (controlScheme)
					{
						case ControlSchemeEnum.Keyboard:
							return FullControlEnum.KeyboardMoveRight;
						default:
							throw new ArgumentOutOfRangeException();
					}
				case GroupedControlEnum.Interact:
					switch (controlScheme)
					{
						case ControlSchemeEnum.Keyboard:
							return FullControlEnum.KeyboardInteract;
						default:
							throw new ArgumentOutOfRangeException();
					}
				case GroupedControlEnum.Shout:
					switch (controlScheme)
					{
						case ControlSchemeEnum.Keyboard:
							return FullControlEnum.KeyboardShout;
						default:
							throw new ArgumentOutOfRangeException();
					}
				case GroupedControlEnum.Journal:
					switch (controlScheme)
					{
						case ControlSchemeEnum.Keyboard:
							return FullControlEnum.KeyboardJournal;
						default:
							throw new ArgumentOutOfRangeException();
					}
				case GroupedControlEnum.Help:
					switch (controlScheme)
					{
						case ControlSchemeEnum.Keyboard:
							return FullControlEnum.KeyboardHelp;
						default:
							throw new ArgumentOutOfRangeException();
					}
				default:
					throw new ArgumentOutOfRangeException(nameof(control), control, null);
			}
		}

		public static GroupedControlEnum GetGroupedControl(FullControlEnum control)
		{
			switch (control)
			{
				case FullControlEnum.KeyboardMoveForward:
					return GroupedControlEnum.MoveForward;
				case FullControlEnum.KeyboardMoveBackward:
					return GroupedControlEnum.MoveBackward;
				case FullControlEnum.KeyboardMoveLeft:
					return GroupedControlEnum.MoveLeft;
				case FullControlEnum.KeyboardMoveRight:
					return GroupedControlEnum.MoveRight;
				case FullControlEnum.KeyboardInteract:
					return GroupedControlEnum.Interact;
				case FullControlEnum.KeyboardShout:
					return GroupedControlEnum.Shout;
				case FullControlEnum.KeyboardJournal:
					return GroupedControlEnum.Journal;
				case FullControlEnum.KeyboardHelp:
					return GroupedControlEnum.Help;
				default:
					throw new ArgumentOutOfRangeException(nameof(control), control, null);
			}
		}

		public static ControlSchemeEnum GetControlScheme(FullControlEnum control)
		{
			switch (control)
			{
				case FullControlEnum.KeyboardMoveForward:
				case FullControlEnum.KeyboardMoveBackward:
				case FullControlEnum.KeyboardMoveLeft:
				case FullControlEnum.KeyboardMoveRight:
				case FullControlEnum.KeyboardInteract:
				case FullControlEnum.KeyboardShout:
				case FullControlEnum.KeyboardJournal:
				case FullControlEnum.KeyboardHelp:
					return ControlSchemeEnum.Keyboard;
				default:
					throw new ArgumentOutOfRangeException(nameof(control), control, null);
			}
		}
		
		public static bool HasSecondary(FullControlEnum control)
		{
			switch (control)
			{
				case FullControlEnum.KeyboardMoveForward:
				case FullControlEnum.KeyboardMoveBackward:
				case FullControlEnum.KeyboardMoveLeft:
				case FullControlEnum.KeyboardMoveRight:
				case FullControlEnum.KeyboardInteract:
				case FullControlEnum.KeyboardShout:
					return true;
				case FullControlEnum.KeyboardJournal:
				case FullControlEnum.KeyboardHelp:
					return false;
				default:
					throw new ArgumentOutOfRangeException(nameof(control), control, null);
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