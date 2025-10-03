using System;
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

			public void ShowButtons(DialogueOption[] options)
			{
				parent.SetActive(true);
				if (options.Length != buttons.Length)
					throw new InvalidOperationException($"This is the wrong button set for {options.Length} buttons!");
				for (var i = 0; i < options.Length; i++) buttons[i].AssignDialogueOption(options[i]);
			}

			public void HideButtons()
			{
				for (var i = 0; i < buttons.Length; i++) buttons[i].ResetDialogueOption();
				parent.SetActive(false);
			}
		}
	}
}