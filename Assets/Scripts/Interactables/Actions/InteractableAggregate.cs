using Attributes;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Aggregate Interactable")]
	public sealed class InteractableAggregate : Interactable
	{
		[MinCount(2)] [SerializeField] private Interactable[] interactables;
		
		public override void Interact()
		{
			for (var i = 0; i < interactables.Length; i++) interactables[i].Interact();
		}
	}
}