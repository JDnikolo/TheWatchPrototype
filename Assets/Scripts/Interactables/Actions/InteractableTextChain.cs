using Callbacks.Text;
using Localization.Text;
using Managers;
using UI.Text;
using UnityEngine;
using Utilities;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Text-Chain Interactable")]
	public sealed class InteractableTextChain : Interactable, ITextWriterFinished
	{
		[SerializeField] private TextChain textChain;
		[SerializeField] private TextWriterFinished textWriterFinished;

		private int m_chainIndex;

		public override void Interact()
		{
			m_chainIndex = 0;
			InputManager.Instance.ForceUIInput();
			DialogueManager.Instance.OpenTextWriter(new TextWriterInput(textChain.TextAssets[m_chainIndex],
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