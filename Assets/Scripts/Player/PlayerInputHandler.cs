using Input;
using Managers;
using UnityEngine;

namespace Player
{
	public sealed class PlayerInputHandler : MonoBehaviour, IStartable
	{
		[SerializeField] private string moveAxisName = "Move";
		[SerializeField] private string lookAxisName = "Look";
		
		public byte StartOrder => 0;

		private InputAxis<Vector2> m_moveAxis = new();
		private InputAxis<Vector2> m_lookAxis = new();
		
		public void OnStart()
		{
			m_moveAxis.AssignAction(InputManager.Instance.GetPlayerAction(moveAxisName));
			m_lookAxis.AssignAction(InputManager.Instance.GetPlayerAction(lookAxisName));
		}

		private void Update()
		{
			m_moveAxis.Update();
			m_lookAxis.Update();
		}

		public bool TryGetMoveAxis(out Vector2 moveAxis)
		{
			if (!m_moveAxis.Assigned)
			{
				moveAxis = Vector2.zero;
				return false;
			}

			moveAxis = m_moveAxis.Value;
			return true;
		}
		
		public bool TryGetLookAxis(out Vector2 lookAxis)
		{
			if (!m_lookAxis.Assigned)
			{
				lookAxis = Vector2.zero;
				return false;
			}
			
			lookAxis = m_lookAxis.Value;
			return true;
		}
	}
}