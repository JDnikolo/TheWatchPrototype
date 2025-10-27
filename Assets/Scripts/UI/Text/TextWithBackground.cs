using Managers;
using TMPro;
using UnityEngine;

namespace UI.Text
{
	[AddComponentMenu("UI/Text/Text With Background")]
	public sealed class TextWithBackground : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI textMesh;
		[SerializeField] private RectTransform background;
		[SerializeField] private float padding = 25f;

		public void SetText(string text)
		{
			textMesh.text = text;
			if (string.IsNullOrEmpty(text)) SetVisible(false);
			else
			{
				SetVisible(true);
				GameManager.Instance.InvokeOnNextFrameUpdate(UpdateBackground);
			}
		}

		private void UpdateBackground()
		{
			var textRect = textMesh.rectTransform;
			UpdateBackgroundWidth(padding * 2f + textRect.sizeDelta.x);
		}

		private void UpdateBackgroundWidth(float width)
		{
			var delta = background.sizeDelta;
			delta.x = width;
			background.sizeDelta = delta;
		}

		private void SetVisible(bool visible)
		{
			background.gameObject.SetActive(visible);
			textMesh.gameObject.SetActive(visible);
		}
	}
}