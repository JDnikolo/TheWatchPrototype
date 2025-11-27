using Agents.Behaviors;
using Callbacks.Agent;
using UnityEngine;

namespace Agents.Starting
{
	[AddComponentMenu("Agents/Starting/Start Cinematic")]
	public sealed class AgentStartCinematic : AgentStart
	{
		[SerializeField] private AgentCinematicAssigned onCinematicAssigned;
		
		protected override void Start()
		{
			var behavior = new MovementCinematicBehavior();
			Brain.StartMovement(behavior);
			if (onCinematicAssigned) onCinematicAssigned.OnAgentCinematicAssigned(Brain, behavior);
		}
	}
}