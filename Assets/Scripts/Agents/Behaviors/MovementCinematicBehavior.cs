using UnityEngine;

namespace Agents.Behaviors
{
	public sealed class MovementCinematicBehavior : MovementBehavior
	{
		private Vector2 m_moveTarget;
		private Vector2 m_rotationTarget;
		private float m_slowDownMultiplier;
		
		public override Vector2 MoveTarget => m_moveTarget;

		public override Vector2 RotationTarget => m_rotationTarget;

		public override float SlowDownMultiplier => m_slowDownMultiplier;
		
		public void SetMoveTarget(Vector2 moveTarget) => m_moveTarget = moveTarget;
		
		public void SetRotationTarget(Vector2 rotationTarget) => m_rotationTarget = rotationTarget;
		
		public void SetSlowDownMultiplier(float slowDownMultiplier) => m_slowDownMultiplier = slowDownMultiplier;
	}
}