using Interactables;
using UnityEngine;

namespace Callbacks.Fade
{
	[AddComponentMenu("Callbacks/Fade/Fade Screen Disappeared")]
	public sealed class FadeScreenDisappeared : FadeScreenFinished
	{
		[SerializeField] private Interactable interactable;
		
		public override void OnFadeScreenFinished(bool fadeDirection)
		{
			if (!fadeDirection && interactable) interactable.Interact();
		}
	}
}