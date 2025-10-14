using Agents;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Agent Interactable")]
	public sealed class InteractableAgent : InteractableToggle
	{
		[SerializeField] private AgentBrain brain;
		
		public override void OnInteractionChanged(bool playerEntered)
		{
			if (playerEntered) brain.StopMovement();
			else brain.RestartMovement();
		}
	}
}