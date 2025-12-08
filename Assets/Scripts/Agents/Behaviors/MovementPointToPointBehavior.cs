using System;
using Attributes;
using Callbacks.Agent;
using Navigation;
using UnityEngine;
using Utilities;

namespace Agents.Behaviors
{
	[Serializable]
	public sealed class MovementPointToPointBehavior : MovementBehavior, 
		IMovementBehaviorUpdate, IMovementBehaviorSelected
	{
		[CanBeNullInPrefab, SerializeField] private NavigationPath navigationPath;
		[SerializeField] private float switchDistance;

		private Vector2 m_target;
		private int m_index;

		public override Vector2 MoveTarget => m_target;

		public override Vector2 RotationTarget => m_target;

		public override float SlowDownMultiplier => 1f;

		public void OnSelected() => m_target = navigationPath[m_index].position.ToFlatVector();

		public void UpdateMovement(Vector3 position)
		{
			if (Vector2.SqrMagnitude(position.ToFlatVector() - m_target) > switchDistance * switchDistance) return;
			m_target = navigationPath.MoveNext(ref m_index).position.ToFlatVector();
		}
#if UNITY_EDITOR
		public override void DisplayInEditor()
		{
			base.DisplayInEditor();
			navigationPath.Display("Navigation Path");
			switchDistance.Display("Switch Distance");
		}
#endif
	}
}