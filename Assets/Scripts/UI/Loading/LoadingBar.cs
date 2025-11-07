using UnityEngine;

namespace UI.Loading
{
	[AddComponentMenu("UI/Loading/Loading Bar")]
	public sealed class LoadingBar : MonoBehaviour
	{
		[SerializeField] private RectTransform loadingRect;
		[SerializeField] private RectTransform fullRect;
		[SerializeField] private float padding = 25f;

		private float InnerWidth => fullRect.sizeDelta.x - padding * 2f;
		
		public void SetProgress(float progress)
		{
			if (progress <= 0f) UpdateBarWidth(0f);
			else
			{
				if (progress >= 1f) progress = 1f;
				UpdateBarWidth(InnerWidth * progress);
			}
		}
		
		private void UpdateBarWidth(float width)
		{
			var delta = loadingRect.sizeDelta;
			delta.x = width;
			loadingRect.sizeDelta = delta;
		}
	}
}