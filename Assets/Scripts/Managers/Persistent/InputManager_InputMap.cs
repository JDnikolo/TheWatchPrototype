using System;
using Input;
using UnityEngine.InputSystem;
using Utilities;

namespace Managers.Persistent
{
	public sealed partial class InputManager
	{
		public sealed class InputMap : IDisposable
		{
			private InputActionMap m_map;
			private ControlMapEnum m_controlMap;
			private int m_bitFlag;

			public InputActionMap Map => m_map ??= Instance.GetMapByControl(m_controlMap);
			
			internal int BitFlag => m_bitFlag;
		
			public bool Enabled
			{
				get => Map.enabled;
				set
				{
					if (Map.enabled == value) return;
					Map.SetEnabled(value);
					Instance.SetFlag(m_bitFlag, value);
				}
			}
		
			public InputMap(ControlMapEnum controlMap)
			{
				m_controlMap = controlMap;
				var bit = (int) controlMap;
				if (bit is < 0 or >= 32) throw new ArgumentOutOfRangeException(nameof(bit));
				m_bitFlag = 1 << bit;
			}
			
			internal void ForceEnable() => Map.Enable();
			
			internal void ForceDisable() => Map.Disable();

			public InputAction GetAction(string name) => Map.FindAction(name);

			public void Dispose()
			{
				m_map = null;
				m_controlMap = default;
				m_bitFlag = 0;
			}
#if UNITY_EDITOR
			public bool EditorEnabled => m_map != null && Enabled;
#endif
		}
	}
}