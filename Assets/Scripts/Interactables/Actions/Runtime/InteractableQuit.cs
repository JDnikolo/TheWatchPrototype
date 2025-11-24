using UnityEngine;

namespace Interactables.Actions.Runtime
{
	[AddComponentMenu("Interactables/Runtime/Quit Game")]
	public sealed class InteractableQuit : Interactable
	{
		public override void Interact() => Application.Quit();
	}
}