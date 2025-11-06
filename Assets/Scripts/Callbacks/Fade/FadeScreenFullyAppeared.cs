using Interactables;
using UnityEngine;

namespace Callbacks.Fade
{
	[AddComponentMenu("Callbacks/Fade/Fade Screen Fully-Appeared")]
	public sealed class FadeScreenFullyAppeared : FadeScreenFinished
	{
		[SerializeField] private Interactable interactable;
		
		public override void OnFadeScreenFinished(bool fadeDirection)
		{
			if (fadeDirection && interactable) interactable.Interact();
		}
	}
}