namespace Callbacks.Fade
{
	public abstract class FadeScreenFinished : BaseBehaviour, IFadeScreenFinished
	{
		public abstract void OnFadeScreenFinished(bool fadeDirection);
	}
}