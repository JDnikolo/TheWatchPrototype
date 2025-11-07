using Managers;
using TMPro;
using UnityEngine;
using Utilities;

namespace UI.Text
{
	[AddComponentMenu("UI/Text/Text With Background")]
	public sealed class TextWithBackground : TextWriter
	{
		[SerializeField] private TextMeshProUGUI textMesh;
		[SerializeField] private RectTransform background;
		[SerializeField] private float padding = 25f;

		public override void WriteText(string text)
		{
			textMesh.text = text;
			if (string.IsNullOrEmpty(text)) SetVisible(false);
			else
			{
				SetVisible(true);
				GameManager.Instance.InvokeOnNextFrameUpdateSafe(UpdateBackground);
			}
		}

		private void UpdateBackground() => UpdateBackgroundWidth(padding * 2f + textMesh.rectTransform.sizeDelta.x);

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