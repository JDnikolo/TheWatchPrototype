using Interactables;
using UnityEngine;

namespace Callbacks.Fade
{
	public abstract class FadeScreenInteractable : FadeScreenFinished
	{
		[SerializeField] private Interactable interactable;

		public sealed override void OnFadeScreenFinished(bool fadeDirection)
		{
			if (AllowInteraction(fadeDirection)) interactable.Interact();
		}
		
		protected abstract bool AllowInteraction(bool fadeDirection);
	}
}