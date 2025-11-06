using Callbacks.Text;
using Localization.Speaker;
using Localization.Text;
using Managers;
using UI.Speaker;
using UI.Text;
using UnityEngine;
using Utilities;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Text-Chain Interactable")]
	public sealed class InteractableTextChain : Interactable, ISpeakerWriterFinished
	{
		[SerializeField] private SpeakerChain textChain;
		[SerializeField] private SpeakerWriterFinished textWriterFinished;

		private int m_chainIndex;

		public override void Interact()
		{
			m_chainIndex = 0;
			InputManager.Instance.ForceUIInput();
			UIManager.Instance.OpenTextWriter(new SpeakerWriterInput(textChain.TextAssets[m_chainIndex],
				m_chainIndex < textChain.TextAssets.Length - 1 ? this : textWriterFinished));
		}

		public void OnTextWriterFinished(SpeakerWriter textWriter)
		{
			m_chainIndex += 1;
			textWriter.WriteText(new SpeakerWriterInput(textChain.TextAssets[m_chainIndex],
				m_chainIndex < textChain.TextAssets.Length - 1 ? this : textWriterFinished));
		}
	}
}