using UI.Dialogue;

namespace Callbacks.Dialogue
{
	public abstract class DialogueWriterFinished : BaseBehaviour, IDialogueWriterFinished
	{
		public abstract void OnDialogueWriterFinsished(DialogueWriter dialogueWriter);
	}
}