using System;
using Attributes;
using Callbacks.Agent;
using UnityEngine;
using Utilities;

namespace Agents.Behaviors
{
	[Serializable]
	public sealed class MovementFollowBehavior : MovementBehavior, IMovementBehaviorUpdate
	{
		[CanBeNullInPrefab, SerializeField] private Transform target;

		private Vector2 m_targetPosition;
		
		public override Vector2 MoveTarget => m_targetPosition;

		public override Vector2 RotationTarget => m_targetPosition;

		public override float SlowDownMultiplier => 4f;
		
		public void UpdateMovement(Vector3 position) => m_targetPosition = target.position.ToFlatVector();
#if UNITY_EDITOR
		public override void DisplayInEditor()
		{
			base.DisplayInEditor();
			target.Display("Target");
		}
#endif
	}
}