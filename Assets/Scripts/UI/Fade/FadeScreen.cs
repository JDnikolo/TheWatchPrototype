using Callbacks.Fade;
using Runtime;
using Runtime.FrameUpdate;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace UI.Fade
{
	[AddComponentMenu("UI/Fade/Fade Screen")]
	public sealed class FadeScreen : BaseBehaviour, IFrameUpdatable
	{
		[SerializeField] private Image image;

		private IFadeScreenFinished m_onFinished;
		private Updatable m_updatable;
		private float m_fadePercentage;
		private float m_fadeDuration;
		private bool m_fadeDirection;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.FadeScreen;

		private void Awake()
		{
			m_fadePercentage = image.color.a;
			image.raycastTarget = m_fadePercentage != 0f;
		}

		public void Fade(FadeScreenInput input)
		{
			m_updatable.SetUpdating(true, this);
			m_fadeDirection = input.FadeDirection;
			m_fadeDuration = input.FadeDuration;
			m_onFinished = input.OnFadeScreenFinished;
		}

		private void OnDestroy()
		{
			m_onFinished = null;
			m_updatable.SetUpdating(false, this);
		}

		public void OnFrameUpdate()
		{
			var deltaTime = Time.deltaTime / m_fadeDuration;
			var stop = false;
			if (m_fadeDirection)
			{
				m_fadePercentage += deltaTime;
				if (m_fadePercentage > 1f)
				{
					stop = true;
					m_fadePercentage = 1f;
				}
			}
			else
			{
				m_fadePercentage -= deltaTime;
				if (m_fadePercentage < 0f)
				{
					stop = true;
					m_fadePercentage = 0f;
				}
			}

			var color = image.color;
			color.a = m_fadePercentage;
			image.color = color;
			if (stop)
			{
				if (m_fadePercentage == 0f) image.raycastTarget = false;
				if (m_onFinished != null) m_onFinished.OnFadeScreenFinished(m_fadeDirection);
				OnDestroy();
			}
		}
	}
}