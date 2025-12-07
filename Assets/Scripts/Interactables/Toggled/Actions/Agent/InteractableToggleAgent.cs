using Agents;
using UnityEngine;

namespace Interactables.Toggled.Actions.Agent
{
	[AddComponentMenu("Interactables/Agent/Toggle Agent Brain")]
	public sealed class InteractableToggleAgent : InteractableToggle
	{
		[SerializeField] private AgentBrain brain;
		
		public override void OnInteractionChanged(bool playerEntered)
		{
			if (playerEntered) brain.StopMovement();
			else brain.RestartMovement();
		}
	}
}