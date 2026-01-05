using System;
using Callbacks.Backing;
using Exceptions;
using Input;
using Runtime;
using Runtime.FrameUpdate;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Managers.Persistent
{
	[AddComponentMenu("Managers/Persistent/Input Manager")]
	public sealed partial class InputManager : Singleton<InputManager>, IFrameUpdatable
	{
		[SerializeField] private InputActionAsset actionAsset;
		[SerializeField] private string playerMapName = "Player";
		[SerializeField] private string uiMapName = "UI";
		[SerializeField] private string persistentGameName = "PersistentGame";
		
		//Input maps
		private InputMap m_playerMap = new(ControlMapFlags.Player);
		private InputMap m_uiMap = new(ControlMapFlags.UI);
		private InputMap m_persistentGameMap = new(ControlMapFlags.PersistentGame);
		
		//Special inputs
		[SerializeField] private SpecialInput<IBackHook> backSpecial;

		//Manager variables
		private ControlSchemeEnum m_controlScheme = ControlSchemeEnum.ENUM_LENGTH;
		private Updatable m_updatable;
		private int m_activeControls;
		private int m_activeSpecials;
		private bool m_cursorVisible = true;
		
		//Group masks
		private const string KeyboardAndMouseGroup = "Keyboard&Mouse";
		private const string GamepadGroup = "Gamepad";
		
		//Runtime
		protected override bool Override => false;
		
		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.InputManager;
		
		//Input maps
		public InputMap PlayerMap => m_playerMap;
		
		public InputMap UIMap => m_uiMap;
		
		public InputMap PersistentGameMap => m_persistentGameMap;
		
		//Specials
		public SpecialInput<IBackHook> BackSpecial => backSpecial;

		//States
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
						actionAsset.bindingMask = InputBinding.MaskByGroup(KeyboardAndMouseGroup);
						break;
					case ControlSchemeEnum.Gamepad:
						actionAsset.bindingMask = InputBinding.MaskByGroup(GamepadGroup);
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
				SetMapFromFlag(m_playerMap);
				SetMapFromFlag(m_uiMap);
				SetMapFromFlag(m_persistentGameMap);
				ToggleCursor(value.CursorVisible);
			}
		}

		//Pointer
		public static Vector2 PointerPosition => Pointer.current.position.ReadValue();

		public static bool IsPointerPressed => Pointer.current.press.isPressed;
		
		public static bool WasPointPressedThisFrame => Pointer.current.press.wasPressedThisFrame;
		
		public static bool WasPointerReleasedThisFrame => Pointer.current.press.wasReleasedThisFrame;

		public void OnFrameUpdate()
		{
			backSpecial.Update(ref m_activeSpecials);
		}
		
		public static InputBindingPair GetBindingIndexes(InputAction action, 
			ControlSchemeEnum scheme, string name = null)
		{
			var mask = InputBinding.MaskByGroup(GetGroup(scheme));
			if (name != null) mask.name = name;
			var bindings = action.bindings;
			int baseIndex;
			for (baseIndex = 0; baseIndex < bindings.Count; baseIndex++)
			{
				var binding = bindings[baseIndex];
				if (!binding.isComposite && mask.Matches(binding)) break;
			}

			return new InputBindingPair(baseIndex, baseIndex + 1);
		}

		public static int GetBindingIndex(InputAction action, bool secondary, 
			ControlSchemeEnum scheme, string name = null)
		{
			var pair = GetBindingIndexes(action, scheme, name);
			return secondary ? pair.Secondary : pair.Primary;
		}
		
		public InputBindingPair GetBindingIndexes(InputAction action, string name = null) => 
			GetBindingIndexes(action, m_controlScheme, name);
		
		public int GetBindingIndex(InputAction action, bool secondary, string name = null) => 
			GetBindingIndex(action, secondary, m_controlScheme, name);

		public static string GetGroup(ControlSchemeEnum scheme)
		{
			switch (scheme)
			{
				case ControlSchemeEnum.Keyboard:
					return KeyboardAndMouseGroup;
				case ControlSchemeEnum.Gamepad:
					return GamepadGroup;
				default:
					throw new ArgumentOutOfRangeException(nameof(scheme), scheme, null);
			}
		}
		
		public void Stop()
		{
			m_playerMap.ForceDisable();
			m_uiMap.ForceDisable();
			m_persistentGameMap.ForceDisable();
			m_activeControls = 0;
		}

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

		private void SetControlFlag(int mask, bool value) => SetFlag(ref m_activeControls, mask, value);
		
		private void SetSpecialFlag(int mask, bool value)
		{
			SetFlag(ref m_activeSpecials, mask, value);
			m_updatable.SetUpdating(m_activeSpecials != 0, this);
		}

		private static void SetFlag(ref int flags, int mask, bool value)
		{
			if (value) flags |= mask;
			else flags &= ~mask;
		}

		private void SetMapFromFlag(InputMap map)
		{
			if ((map.BitFlag & m_activeControls) != 0) map.ForceEnable();
			else map.ForceDisable();
		}
		
		private InputActionMap GetMapByControl(ControlMapFlags map)
		{
			switch (map)
			{
				case ControlMapFlags.Player:
					return actionAsset.FindActionMap(playerMapName);
				case ControlMapFlags.UI:
					return actionAsset.FindActionMap(uiMapName);
				case ControlMapFlags.PersistentGame:
					return actionAsset.FindActionMap(persistentGameName);
				default:
					throw new ArgumentOutOfRangeException(nameof(map), map, null);
			}
		}
		
		protected override void Awake()
		{
			base.Awake();
			byte flag = 0;
			backSpecial.Initialize(BackTriggered, flag++);
			Stop();
		}

		private void BackTriggered(IBackHook arg1, InputAction arg2) => arg1.OnBackPressed(arg2.GetInputState());

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
#if UNITY_EDITOR
		public ControlSchemeEnum ControlSchemeEditor => m_controlScheme;
		
		public int ActiveControls => m_activeControls;
		
		public int ActiveSpecials => m_activeSpecials;
#endif
	}
}