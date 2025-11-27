using AYellowpaper.SerializedCollections;
using Callbacks.Speaker;
using Localization.Speaker;
using Managers;
using Managers.Persistent;
using UI.Speaker;
using UnityEngine;
using Utilities;

namespace Interactables.Actions.Speaker
{
	[AddComponentMenu("Interactables/Speaker/Display Speaker-Chain")]
	public sealed class InteractableSpeakerChain : Interactable, ISpeakerWriterFinished
	{
		[SerializeField] private SpeakerChain textChain;
		[SerializeField] private SerializedDictionary<int, Interactable> onIndexedTextFinished;
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
			if (onIndexedTextFinished.TryGetValue(m_chainIndex, out var interactable)) interactable.Interact();
			textWriter.WriteText(new SpeakerWriterInput(textChain.TextAssets[m_chainIndex],
				m_chainIndex < textChain.TextAssets.Length - 1 ? this : textWriterFinished));
		}
	}
}