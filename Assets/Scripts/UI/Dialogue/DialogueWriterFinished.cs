using UnityEngine;

namespace UI.Dialogue
{
	public abstract class DialogueWriterFinished : MonoBehaviour, IDialogueWriterFinished
	{
		public abstract void OnDialogueWriterFinsished(DialogueWriter dialogueWriter);
	}
}