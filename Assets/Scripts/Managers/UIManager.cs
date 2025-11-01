using UI.Dialogue;
using UI.Text;
using UnityEngine;

namespace Managers
{
	[AddComponentMenu("Managers/UI Manager")]
	public sealed class UIManager : Singleton<UIManager>
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

		public void SkipText()
		{
			if (!textWriter.gameObject.activeInHierarchy) return;
			textWriter.SkipText();
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