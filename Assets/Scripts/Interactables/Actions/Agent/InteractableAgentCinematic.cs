using Agents.Behaviors;
using Callbacks.Agent;
using UnityEngine;

namespace Interactables.Actions.Agent
{
	[AddComponentMenu("Interactables/Make Agent Cinematic")]
	public sealed class InteractableAgentCinematic : InteractableAgent
	{
		[SerializeField] private AgentCinematicAssigned onCinematicAssigned;
		
		public override void Interact()
		{
			if (Agent.MovementBehavior is not MovementCinematicBehavior behavior)
			{
				behavior = new MovementCinematicBehavior();
				Agent.StartMovement(behavior);
			}
		
			onCinematicAssigned.OnAgentCinematicAssigned(Agent, behavior);
		}
	}
}