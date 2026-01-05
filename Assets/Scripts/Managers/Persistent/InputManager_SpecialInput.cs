using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers.Persistent
{
	public sealed partial class InputManager
	{
		[Serializable]
		public struct SpecialInput<T>
		{
			[SerializeField] private InputActionReference inputReference;
			
			private List<T> m_hooks;
			private Action<T, InputAction> m_action;
			private int m_bitFlag;

			public void Initialize(Action<T, InputAction> action, byte flag)
			{
				m_hooks = new List<T>();
				m_action = action;
				m_bitFlag = 1 << flag;
			}
			
			public void AddHook(T hook)
			{
				if (m_hooks == null) return;
				m_hooks.Add(hook);
				Instance.SetSpecialFlag(m_bitFlag, true);
			}

			public void RemoveHook(T hook)
			{
				if (m_hooks == null) return;
				m_hooks.Remove(hook);
				if (m_hooks.Count == 0) Instance.SetSpecialFlag(m_bitFlag, false);
			}
			
			internal void Update(ref int flags)
			{
				if ((flags & m_bitFlag) != 0) m_action(m_hooks[^1], inputReference.action);
			}
#if UNITY_EDITOR
			public bool Enabled => (Instance.m_activeSpecials & m_bitFlag) != 0;
			
			public List<T> Hooks => m_hooks;

			public InputAction Action => inputReference.action;
#endif
		}
	}
}