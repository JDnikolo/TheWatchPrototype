using UI.Dialogue;
using UI.Text;
using UnityEngine;

namespace Managers
{
	[AddComponentMenu("Managers/Dialogue Manager")]
	public sealed class DialogueManager : Singleton<DialogueManager>
	{
		[SerializeField] private TextWriter textWriter;
		[SerializeField] private DialogueWriter dialogueWriter;
		
		public void OpenTextWriter(TextWriterInput input)
		{
			textWriter.gameObject.SetActive(true);
			textWriter.WriteText(input);
		}

		public void CloseTextWriter()
		{
			textWriter.DisposeText();
			textWriter.gameObject.SetActive(false);
		}

		public void OpenDialogueWriter(DialogueWriterInput input)
		{
			dialogueWriter.gameObject.SetActive(true);
			dialogueWriter.WriteDialogue(input);
		}

		public void CloseDialogueWriter()
		{
			dialogueWriter.DisposeDialogue();
			dialogueWriter.gameObject.SetActive(false);
		}
	}
}