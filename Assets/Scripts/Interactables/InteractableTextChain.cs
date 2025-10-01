using Localization;
using Managers;
using UI;
using UnityEngine;

namespace Interactables
{
	public sealed class InteractableTextChain : Interactable, ITextWriterFinished
	{
		[SerializeField] private TextChain textChain;
		[SerializeField] private TextWriterFinished textWriterFinished;

		private int m_chainIndex;

		public override void Interact()
		{
			m_chainIndex = 0;
			InputManager.Instance.ForceUIInput();
			DialogueManager.Instance.OpenDialogue(new TextWriterInput(textChain.TextAssets[m_chainIndex],
				m_chainIndex < textChain.TextAssets.Length ? this : textWriterFinished));
		}

		public void OnTextWriterFinished(TextWriter textWriter)
		{
			m_chainIndex += 1;
			textWriter.WriteText(new TextWriterInput(textChain.TextAssets[m_chainIndex], GetTextWriterFinished()));
		}

		private ITextWriterFinished GetTextWriterFinished() =>
			m_chainIndex < textChain.TextAssets.Length - 1 ? this : textWriterFinished;
	}
}