using UnityEngine.InputSystem;

namespace Input
{
	public sealed class InputAxis<T> where T : struct
	{
		private InputAction m_action;
		private T m_value;
		
		public T Value => m_value;
		
		public bool Assigned => m_action != null;

		public void AssignAction(InputAction action) => m_action = action;

		public void Update()
		{
			if (m_action == null || !m_action.enabled) m_value = default;
			else m_value = m_action.ReadValue<T>();
		}
	}
}