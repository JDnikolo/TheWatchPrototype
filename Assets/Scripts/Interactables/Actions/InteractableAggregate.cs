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
			var length = interactables.Length;
			for (var i = 0; i < length; i++)
			{
				var interactable = interactables[i];
				if (!interactable)
				{
					Debug.LogError($"Interactable at {i} was null!", this);
					return;
				}
				
				interactable.Interact();
			}
		}
	}
}