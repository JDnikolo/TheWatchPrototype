using Agents.Behaviors;
using UnityEngine;

namespace Interactables.Actions.Agent
{
	[AddComponentMenu("Interactables/Make Agent Follow")]
	public sealed class InteractableAgentFollow : InteractableAgent
	{
		[SerializeField] private MovementFollowBehavior followBehavior;
		
		public override void Interact() => Agent.StartMovement(followBehavior);
	}
}