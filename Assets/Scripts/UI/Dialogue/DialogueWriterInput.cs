using Localization.Dialogue;
using Localization.Text;

namespace UI.Dialogue
{
	public struct DialogueWriterInput
	{
		public DialogueObject DialogueToDisplay;
		public TextObject QuestionToDisplay;
		public IDialogueWriterFinished OnDialogueWriterFinished;

		public static readonly DialogueWriterInput Empty = new();
		
		public DialogueWriterInput(DialogueObject dialogueToDisplay, TextObject questionToDisplay, 
			IDialogueWriterFinished onDialogueWriterFinished)
		{
			DialogueToDisplay = dialogueToDisplay;
			QuestionToDisplay = questionToDisplay;
			OnDialogueWriterFinished = onDialogueWriterFinished;
		}
	}
}