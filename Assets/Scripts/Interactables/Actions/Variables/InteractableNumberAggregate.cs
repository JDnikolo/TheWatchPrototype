using AYellowpaper.SerializedCollections;
using UnityEngine;
using Variables;

namespace Interactables.Actions.Variables
{
	[AddComponentMenu("Interactables/Variable/Number Variable Selector")]
	public sealed class InteractableNumberAggregate : Interactable
	{
		//[MinCount(2)] 
		[SerializeField] private SerializedDictionary<int, Interactable> interactables;
		[SerializeField] private NumberVariable variable;
		
		public override void Interact()
		{
			if (interactables.TryGetValue(variable.Value, out var interactable)) interactable.Interact();
		}
	}
}