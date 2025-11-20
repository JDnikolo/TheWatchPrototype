using Input;
using Managers.Persistent;
using Runtime.FrameUpdate;
using UnityEngine;
using Utilities;

namespace Player
{
	[AddComponentMenu("Player/Player Input Handler")]
	public sealed class PlayerInputHandler : MonoBehaviour, IFrameUpdatable
	{
		[SerializeField] private new Rigidbody rigidbody;
		[SerializeField] private string moveAxisName = "Move";
		[SerializeField] private string lookAxisName = "Look";
		
		public Rigidbody Rigidbody => rigidbody;

		private InputAxis<Vector2> m_moveAxis;
		private InputAxis<Vector2> m_lookAxis;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.Player;

		public void OnFrameUpdate()
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
		
		private void Awake() => GameManager.Instance.AddFrameUpdateSafe(this);

		private void OnDestroy() => GameManager.Instance.RemoveFrameUpdateSafe(this);
	}
}