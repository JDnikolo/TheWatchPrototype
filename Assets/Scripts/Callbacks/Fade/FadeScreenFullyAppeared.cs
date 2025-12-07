using UnityEngine;

namespace Callbacks.Fade
{
	[AddComponentMenu("Callbacks/Fade/Fade Screen Fully-Appeared")]
	public sealed class FadeScreenFullyAppeared : FadeScreenInteractable
	{
		protected override bool AllowInteraction(bool fadeDirection) => fadeDirection;
	}
}