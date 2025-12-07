using Attributes;
using Audio;
using Managers.Persistent;
using Runtime;
using UI.Dialogue;
using UI.Fade;
using UI.Speaker;
using UnityEngine;
using UnityEngine.UI;
using LayoutElement = UI.Layout.LayoutElement;

namespace Managers
{
	[AddComponentMenu("Managers/UI Manager")]
	public sealed class UIManager : Singleton<UIManager>
	{
		[Header("Canvas")] 
		// ReSharper disable once MissingLinebreak
		[CanBeNullInPrefab, SerializeField] private RectTransform canvasRect;
		[CanBeNullInPrefab, SerializeField] private GraphicRaycaster raycaster;

		[Header("Fade")] 
		// ReSharper disable once MissingLinebreak
		[CanBeNullInPrefab, SerializeField] private FadeScreen fadeScreen;
		[CanBeNullInPrefab, SerializeField] private float fadeDuration = 1f;

		[Header("Game UI")] 
		// ReSharper disable once MissingLinebreak
		[CanBeNull, SerializeField] private SpeakerWriter textWriter;
		[CanBeNull, SerializeField] private DialogueWriter dialogueWriter;

		[Header("Misc")] 
		// ReSharper disable once MissingLinebreak
		[CanBeNull, SerializeField] private LayoutElement controlPanel;
		[CanBeNull, SerializeField] private AudioSnapshot speakerSnapshot;
		
		private AudioManager.State m_state;
		private bool m_stateSet;
		
		protected override bool Override => true;

		public RectTransform CanvasRect => canvasRect;

		public GraphicRaycaster Raycaster => raycaster;

		public LayoutElement ControlPanel => controlPanel;

		public void ReturnSpeaker()
		{
			if (!m_stateSet) return;
			m_stateSet = false;
			AudioManager.Instance.PauseState = m_state;
		}

		public void OpenTextWriter(SpeakerWriterInput input)
		{
			var textObject = textWriter.gameObject;
			if (!textObject.activeInHierarchy)
			{
				textObject.SetActive(true);
				GameManager.Instance.AddFrameUpdate(textWriter);
			}

			if (input.TextToDisplay.Audio && !m_stateSet)
			{
				m_stateSet = true;
				var audioManager = AudioManager.Instance;
				audioManager.PreparePause();
				m_state = audioManager.PauseState;
				audioManager.SetSnapshot(speakerSnapshot, false, 0.1f, 0.5f);
			}

			textWriter.WriteText(input);
		}

		public void CloseTextWriter()
		{
			var textObject = textWriter.gameObject;
			if (!textObject.activeInHierarchy) return;
			textWriter.DisposeText();
			GameManager.Instance.RemoveFrameUpdate(textWriter);
			textObject.SetActive(false);
		}

		public void OpenDialogueWriter(DialogueWriterInput input)
		{
			var dialogueObject = dialogueWriter.gameObject;
			if (!dialogueObject.activeInHierarchy) dialogueObject.SetActive(true);
			dialogueWriter.WriteDialogue(input);
		}

		public void CloseDialogueWriter()
		{
			var dialogueObject = dialogueWriter.gameObject;
			if (!dialogueObject.activeInHierarchy) return;
			dialogueWriter.DisposeDialogue();
			dialogueObject.SetActive(false);
		}

		public void FadeScreen(FadeScreenInput input)
		{
			if (input.FadeDuration < 0) input.FadeDuration = fadeDuration;
			fadeScreen.Fade(input);
		}
	}
}