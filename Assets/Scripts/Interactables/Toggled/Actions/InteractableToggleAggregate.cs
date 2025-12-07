using Attributes;
using UnityEngine;

namespace Interactables.Toggled.Actions
{
	[AddComponentMenu("Interactables/Actions/Aggregate Toggled Interactable")]
	public sealed class InteractableToggleAggregate : InteractableToggle
	{
		[MinCount(2)] [SerializeField] private InteractableToggle[] interactables;
		
		public override void OnInteractionChanged(bool playerEntered)
		{
			for (var i = 0; i < interactables.Length; i++) interactables[i].OnInteractionChanged(playerEntered);
		}
	}
}