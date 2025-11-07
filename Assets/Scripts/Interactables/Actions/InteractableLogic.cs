using Logic;
using Logic.Boolean;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Logic Interactable")]
	public sealed class InteractableLogic : Interactable
	{
		[SerializeField] private Interactable interactable;
		[SerializeField] private LogicBoolean logicGate;

		public override void Interact()
		{
			if (logicGate.Evaluate() && interactable) interactable.Interact();
		}
	}
}