using UnityEngine;
using Variables;

namespace Interactables.Actions.Variables
{
	[AddComponentMenu("Interactables/Variables/Set Number Variable")]
	public sealed class InteractableNumber : Interactable
	{
		[SerializeField] private NumberVariable variable;
		[SerializeField] private int targetValue;
		
		public override void Interact() => variable.Value = targetValue;
	}
}