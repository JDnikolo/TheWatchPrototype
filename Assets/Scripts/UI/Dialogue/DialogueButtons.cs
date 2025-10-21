using System;
using System.Collections.Generic;
using Localization.Dialogue;
using UnityEngine;

namespace UI.Dialogue
{
	public partial class DialogueWriter
	{
		[Serializable]
		private struct DialogueButtons
		{
			[SerializeField] private DialogueButton[] buttons;
			[SerializeField] private GameObject parent;

			public void ShowButtons(IList<DialogueOption> options)
			{
				parent.SetActive(true);
				if (options.Count != buttons.Length)
					throw new InvalidOperationException($"This is the wrong button set for {options.Count} buttons!");
				for (var i = 0; i < options.Count; i++) ShowButton(options[i], i);
			}

			public void ShowButton(DialogueOption option, int index) => buttons[index].AssignDialogueOption(option);

			public void HideButtons()
			{
				for (var i = 0; i < buttons.Length; i++) buttons[i].ResetDialogueOption();
				parent.SetActive(false);
			}
		}
	}
}