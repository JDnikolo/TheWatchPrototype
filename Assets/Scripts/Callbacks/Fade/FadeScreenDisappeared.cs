using UnityEngine;

namespace Callbacks.Fade
{
	[AddComponentMenu("Callbacks/Fade/Fade Screen Disappeared")]
	public sealed class FadeScreenDisappeared : FadeScreenInteractable
	{
		protected override bool AllowInteraction(bool fadeDirection) => !fadeDirection;
	}
}