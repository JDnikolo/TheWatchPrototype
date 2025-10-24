﻿using System;
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
				textWriter.SetText(text);
			}

			public void HideText()
			{
				textWriter.SetText(null);
				parent.SetActive(false);
			}
		}
	}
}