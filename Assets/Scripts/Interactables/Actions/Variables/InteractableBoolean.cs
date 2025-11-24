using UnityEngine;
using Variables;

namespace Interactables.Actions.Variables
{
	[AddComponentMenu("Interactables/Variables/Set Boolean Variable")]
	public sealed class InteractableBoolean : Interactable
	{
		[SerializeField] private BooleanVariable variable;
		[SerializeField] private bool targetValue;
		
		public override void Interact() => variable.Value = targetValue;
	}
}