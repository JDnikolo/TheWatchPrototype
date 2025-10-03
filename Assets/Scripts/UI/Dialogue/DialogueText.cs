using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialogue
{
	public partial class DialogueWriter
	{
		[Serializable]
		public struct DialogueText
		{
			[SerializeField] private TextMeshProUGUI textMesh;
			[SerializeField] private ContentSizeFitter panelFitter;
			[SerializeField] private GameObject parent;

			public void ShowText(string text)
			{
				parent.SetActive(true);
				textMesh.text = text;
				GameManager.Instance.InvokeOnNextUpdate(UpdateRects);
			}

			public void HideText()
			{
				textMesh.text = null;
				parent.SetActive(false);
			}

			private void UpdateRects() => panelFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		}
	}
}