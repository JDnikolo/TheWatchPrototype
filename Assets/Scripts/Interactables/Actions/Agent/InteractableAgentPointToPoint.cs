using Agents.Behaviors;
using UnityEngine;

namespace Interactables.Actions.Agent
{
	[AddComponentMenu("Interactables/Make Agent Point-To-Point")]
	public sealed class InteractableAgentPointToPoint : InteractableAgent
	{
		[SerializeField] private MovementPointToPointBehavior pointToPointBehavior;
		
		public override void Interact() => Agent.StartMovement(pointToPointBehavior);
	}
}