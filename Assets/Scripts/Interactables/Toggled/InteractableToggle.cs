using Callbacks.Interaction;

namespace Interactables.Toggled
{
	public abstract class InteractableToggle : BaseBehaviour, IInteractionChanged
	{
		public abstract void OnInteractionChanged(bool playerEntered);
	}
}