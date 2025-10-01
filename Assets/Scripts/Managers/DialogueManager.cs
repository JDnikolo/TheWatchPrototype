using UI;
using UnityEngine;

namespace Managers
{
	public sealed class DialogueManager : Singleton<DialogueManager>
	{
		[SerializeField] private TextWriter textWriter;
		
		public TextWriter TextWriter => textWriter;
		
		public void OpenDialogue(TextWriterInput textWriterInput)
		{
			textWriter.gameObject.SetActive(true);
			textWriter.WriteText(textWriterInput);
		}

		public void CloseDialogue()
		{
			textWriter.DisposeText();
			textWriter.gameObject.SetActive(false);
		}
	}
}