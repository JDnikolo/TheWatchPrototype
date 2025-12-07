using Attributes;
using Logic.Boolean;
using UnityEngine;

namespace Interactables.Actions.Logic
{
	[AddComponentMenu("Interactables/Logic/Evaluate Logic Gate")]
	public sealed class InteractableLogic : Interactable
	{
		[CanBeNullInPath("DialogueBuildingBlocks"), SerializeField]
		private LogicBoolean logicGate;

		[CanBeNullInPath("DialogueBuildingBlocks"), SerializeField]
		private Interactable interactable;

		public override void Interact()
		{
			if (logicGate.Evaluate()) interactable.Interact();
		}
	}
}