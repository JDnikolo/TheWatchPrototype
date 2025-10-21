using UnityEngine;
using Variables;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Number Interactable")]
	public sealed class InteractableNumber : Interactable
	{
		[SerializeField] private NumberVariable variable;
		[SerializeField] private int targetValue;
		
		public override void Interact() => variable.Value = targetValue;
	}
}