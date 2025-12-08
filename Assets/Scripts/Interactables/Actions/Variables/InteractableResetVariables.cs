using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions.Variables
{
	[AddComponentMenu("Interactables/Variables/Reset All Variables")]
	public sealed class InteractableResetVariables : Interactable
	{
		public override void Interact() => VariableManager.Instance.ResetVariables();
	}
}