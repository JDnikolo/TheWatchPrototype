using System;
using UI.Text;
using UnityEngine;

namespace UI.Dialogue
{
	public partial class DialogueWriter
	{
		[Serializable]
		public struct DialogueText
		{
			[SerializeField] private TextWithBackground textWriter;
			[SerializeField] private GameObject parent;

			public void ShowText(string text)
			{
				parent.SetActive(true);
				textWriter.WriteText(text);
			}

			public void HideText()
			{
				textWriter.WriteText(null);
				parent.SetActive(false);
			}
		}
	}
}