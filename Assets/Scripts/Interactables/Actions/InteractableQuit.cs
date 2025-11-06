using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Quit Game")]
	public sealed class InteractableQuit : Interactable
	{
		public override void Interact() => Application.Quit();
	}
}