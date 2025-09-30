using UnityEngine;

namespace Interactables
{
	public abstract class InteractableToggle : MonoBehaviour
	{
		public abstract void OnInteractionChanged(bool playerEntered);
	}
}