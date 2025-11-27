using Agents;
using Agents.Behaviors;
using Callbacks.Agent;
using UnityEngine;

namespace Interactables.Actions.Agent
{
	[AddComponentMenu("Interactables/Make Agent Cinematic")]
	public sealed class InteractableAgentCinematic : Interactable
	{
		[SerializeField] private AgentBrain agent;
		[SerializeField] private AgentCinematicAssigned onCinematicAssigned;
		
		public override void Interact()
		{
			if (!agent) return;
			if (agent.MovementBehavior is not MovementCinematicBehavior behavior)
			{
				behavior = new MovementCinematicBehavior();
				agent.StartMovement(behavior);
			}
		
			if (onCinematicAssigned) onCinematicAssigned.OnAgentCinematicAssigned(agent, behavior);
		}
	}
}