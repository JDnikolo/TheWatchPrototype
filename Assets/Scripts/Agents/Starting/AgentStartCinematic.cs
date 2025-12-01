using Agents.Behaviors;
using Callbacks.Agent;
using UnityEngine;

namespace Agents.Starting
{
	[AddComponentMenu("Agents/Starting/Start Cinematic")]
	public sealed class AgentStartCinematic : AgentStart
	{
		[SerializeField] private AgentCinematicAssigned onCinematicAssigned;
		[SerializeField] private Transform lookAtTarget;
		[SerializeField] private Transform moveTarget;
		protected override void Start()
		{
			var behavior = new MovementCinematicBehavior();
			behavior.SetMoveTarget(new Vector2(moveTarget.position.x, moveTarget.position.z));
			behavior.SetRotationTarget(new Vector2(moveTarget.position.x, moveTarget.position.z));
			behavior.SetSlowDownMultiplier(4f);
			Brain.StartMovement(behavior);
			if (onCinematicAssigned) onCinematicAssigned.OnAgentCinematicAssigned(Brain, behavior);
		}
	}
}