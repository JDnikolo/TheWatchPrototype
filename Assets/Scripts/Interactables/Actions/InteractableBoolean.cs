using UnityEngine;
using Variables;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Boolean Interactable")]
	public sealed class InteractableBoolean : Interactable
	{
		[SerializeField] private BooleanVariable variable;
		[SerializeField] private bool targetValue;
		
		public override void Interact() => variable.Value = targetValue;
	}
}