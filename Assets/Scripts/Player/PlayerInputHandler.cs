using Input;
using Managers;
using UnityEngine;

namespace Player
{
	[AddComponentMenu(menuName: "Player/Player Input Handler")]
	public sealed class PlayerInputHandler : MonoBehaviour
	{
		[SerializeField] private string moveAxisName = "Move";
		[SerializeField] private string lookAxisName = "Look";
		
		public byte StartOrder => 0;

		private InputAxis<Vector2> m_moveAxis;
		private InputAxis<Vector2> m_lookAxis;

		private void Update()
		{
			if (!m_moveAxis.Assigned) m_moveAxis.AssignAction(InputManager.Instance.GetPlayerAction(moveAxisName));
			m_moveAxis.Update();
			if (!m_lookAxis.Assigned) m_lookAxis.AssignAction(InputManager.Instance.GetPlayerAction(lookAxisName));
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