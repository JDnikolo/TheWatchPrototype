using Agents.Behaviors;
using Callbacks.Agent;
using UnityEngine;
using Utilities;

namespace Agents.Cinematic
{
	[AddComponentMenu("Agents/Cinematic/Static Cinematic")]
	public sealed class AgentStaticCinematic : AgentCinematicAssigned
	{
		[SerializeField] private Transform lookAtTarget;
		[SerializeField] private Transform moveTarget;
		[SerializeField] private float slowDownMultiplier;
		
		public override void OnAgentCinematicAssigned(AgentBrain agent, MovementCinematicBehavior behavior)
		{
			behavior.SetMoveTarget(moveTarget.position.ToFlatVector());
			behavior.SetRotationTarget(lookAtTarget.position.ToFlatVector());
			behavior.SetSlowDownMultiplier(slowDownMultiplier);
		}
	}
}