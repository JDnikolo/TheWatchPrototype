using Managers.Persistent;
using Runtime;
using UI.Dialogue;
using UI.Fade;
using UI.Speaker;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Managers
{
	[AddComponentMenu("Managers/UI Manager")]
	public sealed class UIManager : Singleton<UIManager>
	{
		[SerializeField] private RectTransform canvasRect;
		[SerializeField] private GraphicRaycaster raycaster;
		[SerializeField] private FadeScreen fadeScreen;
		[SerializeField] private float fadeDuration = 1f;
		
		[SerializeField] private SpeakerWriter textWriter;
		[SerializeField] private DialogueWriter dialogueWriter;
		
		protected override bool Override => true;
		
		public RectTransform CanvasRect => canvasRect;
		
		public GraphicRaycaster Raycaster => raycaster;
		
		public void OpenTextWriter(SpeakerWriterInput input)
		{
			var textObject = textWriter.gameObject;
			if (!textObject.activeInHierarchy)
			{
				textObject.SetActive(true);
				GameManager.Instance.AddFrameUpdateSafe(textWriter);
			}
	
			textWriter.WriteText(input);
		}

		public void CloseTextWriter()
		{
			var textObject = textWriter.gameObject;
			if (!textObject.activeInHierarchy) return;
			textWriter.DisposeText();
			GameManager.Instance.RemoveFrameUpdateSafe(textWriter);
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