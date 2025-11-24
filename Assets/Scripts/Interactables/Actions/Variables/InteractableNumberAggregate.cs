using UnityEngine;
using Variables;

namespace Interactables.Actions.Variables
{
	[AddComponentMenu("Interactables/Variable/Number Variable Selector")]
	public sealed class InteractableNumberAggregate : Interactable
	{
		[SerializeField] private Interactable[] interactables;
		[SerializeField] private NumberVariable variable;
		
		public override void Interact()
		{
			var number = variable.Value;
			if (interactables == null) return;
			var length = interactables.Length;
			if (length == 0) return;
			if (number >= 0 && number < length) interactables[number].Interact();
		}
	}
}