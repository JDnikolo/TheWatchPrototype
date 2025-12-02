using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Aggregate Interactable")]
	public sealed class InteractableAggregate : Interactable
	{
		[SerializeField] private Interactable[] interactables;
		
		public override void Interact()
		{
			if (interactables == null) return;
			var length = interactables.Length;
			if (length == 0) return;
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