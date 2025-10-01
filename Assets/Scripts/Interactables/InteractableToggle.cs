using UnityEngine;

namespace Interactables
{
	public abstract class InteractableToggle : MonoBehaviour, IInteractionChanged
	{
		public abstract void OnInteractionChanged(bool playerEntered);
	}
}