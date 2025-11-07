using UnityEngine;

namespace Callbacks.Fade
{
	public abstract class FadeScreenFinished : MonoBehaviour, IFadeScreenFinished
	{
		public abstract void OnFadeScreenFinished(bool fadeDirection);
	}
}