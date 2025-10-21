using System;
using System.Collections.Generic;
using Localization.Dialogue;
using Localization.Text;
using Managers;
using UnityEngine;
using Utilities;

namespace UI.Dialogue
{
	[AddComponentMenu("UI/Dialogue/Dialogue Writer")]
	public partial class DialogueWriter : MonoBehaviour
	{
		[SerializeField] private DialogueButtons[] buttonSets;
		[SerializeField] private DialogueText choiceText;
		[SerializeField] private DialogueText questionText;

		private List<DialogueOption> m_visibleOptions = new();
		private DialogueOption m_selectedOption;
		private TextObject m_choiceToDisplay;
		private DialogueWriterInput m_input;
		private int m_buttonsInUse = -1;
		
		public DialogueOption SelectedOption => m_selectedOption;
		
		public void WriteDialogue(DialogueWriterInput input)
		{
			m_input = input;
			questionText.ShowText(m_input.QuestionToDisplay.Text);
			ResetWriter();
			m_buttonsInUse = 0;
			var options = m_input.DialogueToDisplay.Options;
			var optionsLength = options.Length;
			if (optionsLength < 1 || optionsLength > buttonSets.Length)
				throw new InvalidOperationException($"Dialogue writer unable to handle {optionsLength} options!");
			for (var i = 0; i < options.Length; i++)
			{
				var option = options[i];
				if (!option.Visible) continue;
				m_visibleOptions.Add(option);
				m_buttonsInUse += 1;
			}
			
			if (m_buttonsInUse == 0)
				throw new InvalidOperationException("Dialogue writer unable to handle 0 visible options!");
			m_buttonsInUse -= 1;
			buttonSets[m_buttonsInUse].ShowButtons(m_visibleOptions);
			m_visibleOptions.Clear();
		}

		public void DisposeDialogue()
		{
			m_input = DialogueWriterInput.Empty;
			questionText.HideText();
			if (buttonSets.ContainsKey(m_buttonsInUse)) buttonSets[m_buttonsInUse].HideButtons();
			ResetWriter();
		}

		private void ResetWriter()
		{
			choiceText.HideText();
			m_selectedOption = null;
			m_choiceToDisplay = null;
			m_buttonsInUse = -1;
			m_visibleOptions.Clear();
		}
		
		public void DisplayOption(DialogueOption option)
		{
			m_choiceToDisplay = option.TextToDisplay;
			choiceText.ShowText(m_choiceToDisplay.Text);
		}

		public void ResetOption(DialogueOption option)
		{
			if (option.TextToDisplay != m_choiceToDisplay) return;
			choiceText.HideText();
			m_choiceToDisplay = null;
		}

		public void SelectOption(DialogueOption option)
		{
			m_selectedOption = option;
			if (m_input.OnDialogueWriterFinished != null)
				m_input.OnDialogueWriterFinished.OnDialogueWriterFinsished(this);
			else
			{
				DialogueManager.Instance.CloseDialogueWriter();
				InputManager.Instance.ForcePlayerInput();
			}
		}
	}
}