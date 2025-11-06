using Callbacks.Dialogue.Selectors;
using UI.Dialogue;
using UnityEngine;

namespace Callbacks.Dialogue
{
	[AddComponentMenu("Callbacks/Dialogue/Selector Dialogue Finished")]
	public class DialogueWriterFinishedSelector : DialogueWriterFinished
	{
		[SerializeField] private DialogueSelector selector;
		
		public override void OnDialogueWriterFinsished(DialogueWriter dialogueWriter)
		{
			if (selector) selector.Evaluate(dialogueWriter.SelectedOption);
		}
	}
}