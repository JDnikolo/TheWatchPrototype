using Interactables;
using UnityEngine;

namespace Callbacks.Fade
{
	[AddComponentMenu("Callbacks/Fade/Fade Screen Finished")]
	public sealed class FadeScreenAny : FadeScreenFinished
	{
		[SerializeField] private Interactable interactable;
		
		public override void OnFadeScreenFinished(bool fadeDirection)
		{
			if (interactable) interactable.Interact();
		}
	}
}