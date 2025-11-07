using UI.Dialogue;
using UI.Fade;
using UI.Speaker;
using UnityEngine;
using Utilities;

namespace Managers
{
	[AddComponentMenu("Managers/UI Manager")]
	public sealed class UIManager : Singleton<UIManager>
	{
		[SerializeField] private FadeScreen fadeScreen;
		[SerializeField] private float fadeDuration = 1f;
		
		[SerializeField] private SpeakerWriter textWriter;
		[SerializeField] private DialogueWriter dialogueWriter;
		
		protected override bool Override => true;
		
		public void OpenTextWriter(SpeakerWriterInput input)
		{
			if (!textWriter.gameObject.activeInHierarchy)
			{
				textWriter.gameObject.SetActive(true);
				GameManager.Instance.AddFrameUpdateSafe(textWriter);
			}
	
			textWriter.WriteText(input);
		}

		public void CloseTextWriter()
		{
			if (!textWriter.gameObject.activeInHierarchy) return;
			textWriter.DisposeText();
			GameManager.Instance.RemoveFrameUpdateSafe(textWriter);
			textWriter.gameObject.SetActive(false);
		}

		public void SkipSpeaker()
		{
			if (!textWriter.gameObject.activeInHierarchy) return;
			textWriter.SkipText();
		}
		
		public void OpenDialogueWriter(DialogueWriterInput input)
		{
			if (!dialogueWriter.gameObject.activeInHierarchy) dialogueWriter.gameObject.SetActive(true);
			dialogueWriter.WriteDialogue(input);
		}

		public void CloseDialogueWriter()
		{
			if (!dialogueWriter.gameObject.activeInHierarchy) return;
			dialogueWriter.DisposeDialogue();
			dialogueWriter.gameObject.SetActive(false);
		}

		public void FadeScreen(FadeScreenInput input)
		{
			if (input.FadeDuration < 0) input.FadeDuration = fadeDuration;
			fadeScreen.Fade(input);
		}
	}
}