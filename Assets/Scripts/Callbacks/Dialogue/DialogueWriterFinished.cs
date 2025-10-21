using UI.Dialogue;
using UnityEngine;

namespace Callbacks.Dialogue
{
	public abstract class DialogueWriterFinished : MonoBehaviour, IDialogueWriterFinished
	{
		public abstract void OnDialogueWriterFinsished(DialogueWriter dialogueWriter);
	}
}