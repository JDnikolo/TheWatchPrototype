using Callbacks.Fade;

namespace UI.Fade
{
	public struct FadeScreenInput
	{
		public bool FadeDirection;
		public float FadeDuration;
		public IFadeScreenFinished OnFadeScreenFinished;

		public FadeScreenInput(bool fadeDirection, float fadeDuration, IFadeScreenFinished onFadeScreenFinished)
		{
			FadeDirection = fadeDirection;
			FadeDuration = fadeDuration;
			OnFadeScreenFinished = onFadeScreenFinished;
		}
	}
}