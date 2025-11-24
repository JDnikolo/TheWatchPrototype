using Callbacks.Dialogue;
using Localization.Dialogue;
using Localization.Speaker;
using Managers;
using Managers.Persistent;
using UI.Dialogue;
using UnityEngine;
using Utilities;

namespace Interactables.Actions.Dialogue
{
	[AddComponentMenu("Interactables/Dialogue/Display Dialogue")]
	public sealed class InteractableDialogueObject : Interactable
	{
		[SerializeField] private DialogueObject dialogueObject;
		[SerializeField] private SpeakerObject questionToDisplay;
		[SerializeField] private DialogueWriterFinished dialogueWriterFinished;
		
		public override void Interact()
		{
			InputManager.Instance.ForceUIInput();
			UIManager.Instance.OpenDialogueWriter(new DialogueWriterInput(
				dialogueObject, questionToDisplay, dialogueWriterFinished));
		}
	}
}