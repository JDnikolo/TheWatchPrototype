using Callbacks.Agent;
using Managers;
using UnityEngine;
using Utilities;

namespace Agents.Behaviors
{
	public sealed class MovementStopBehavior : MovementBehavior, IMovementBehaviorUpdate
	{
		private MovementBehavior m_previousBehavior;
		private Vector2 m_moveTarget;
		private Vector2 m_rotationTarget;

		public override Vector2 MoveTarget => m_moveTarget;

		public override Vector2 RotationTarget => m_rotationTarget;

		public override float SlowDownMultiplier => 2f;

		public void UpdateMovement(Vector3 position)
		{
			var playerManager = PlayerManager.Instance;
			if (playerManager) m_rotationTarget = playerManager.PlayerObject.transform.position.ToFlatVector();
		}
		
		public void Stop(MovementBehavior previousBehavior, Vector2 movePosition, Vector2 velocity)
		{
			if (previousBehavior is MovementStopBehavior) return;
			m_previousBehavior = previousBehavior;
			m_moveTarget = movePosition;
			m_rotationTarget = movePosition + velocity.normalized;
		}

		public MovementBehavior Restart()
		{
			m_moveTarget = Vector3.zero;
			var behavior = m_previousBehavior;
			m_previousBehavior = null;
			return behavior;
		}
#if UNITY_EDITOR
		public override void DisplayInEditor()
		{
			base.DisplayInEditor();
			m_previousBehavior.DisplayInEditor("Previous Behaviour");
		}
#endif
	}
}