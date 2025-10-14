using System;
using UnityEngine;
using Utilities;

namespace Agents.Behaviors
{
	[Serializable]
	public sealed class MovementFollowBehavior : MovementBehavior, IMovementBehaviorUpdate
	{
		[SerializeField] private Transform target;

		private Vector2 m_targetPosition;
		
		public override Vector2 MoveTarget => m_targetPosition;

		public override Vector2 RotationTarget => m_targetPosition;

		public override float SlowDownMultiplier => 4f;

		public void SetTarget(Transform target) => this.target = target;
		
		public void UpdateMovement(Vector3 position) => m_targetPosition = target.position.ToFlatVector();
	}
}