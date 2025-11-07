using Callbacks.Dialogue;
using Localization.Dialogue;
using Localization.Speaker;
using Localization.Text;

namespace UI.Dialogue
{
	public struct DialogueWriterInput
	{
		public DialogueObject DialogueToDisplay;
		public SpeakerObject QuestionToDisplay;
		public IDialogueWriterFinished OnDialogueWriterFinished;

		public static readonly DialogueWriterInput Empty = new();
		
		public DialogueWriterInput(DialogueObject dialogueToDisplay, SpeakerObject questionToDisplay, 
			IDialogueWriterFinished onDialogueWriterFinished)
		{
			DialogueToDisplay = dialogueToDisplay;
			QuestionToDisplay = questionToDisplay;
			OnDialogueWriterFinished = onDialogueWriterFinished;
		}
	}
}