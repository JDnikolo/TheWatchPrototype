using Callbacks.Interaction;

namespace Interactables
{
	public abstract class Interactable : BaseBehaviour, IInteractable
	{
		public abstract void Interact();
	}
}