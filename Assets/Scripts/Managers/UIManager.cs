using UI;
using UnityEngine;

namespace Managers
{
	public sealed class UIManager : Singleton<UIManager>
	{
		[SerializeField] private TextWriter textWriter;

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