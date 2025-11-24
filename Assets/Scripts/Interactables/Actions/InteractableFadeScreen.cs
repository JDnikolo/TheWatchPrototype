using Callbacks.Fade;
using Managers;
using UI.Fade;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Toggle Fade Screen")]
	public sealed class InteractableFadeScreen : Interactable
	{
		[SerializeField] private bool fadeDirection;
		[SerializeField] private float fadeDuration = -1f;
		[SerializeField] private FadeScreenFinished onFadeScreenFinished;
		
		public override void Interact()
		{
			var uiManager = UIManager.Instance;
			if (uiManager) uiManager.FadeScreen(new FadeScreenInput(fadeDirection, fadeDuration, onFadeScreenFinished));
		}
	}
}