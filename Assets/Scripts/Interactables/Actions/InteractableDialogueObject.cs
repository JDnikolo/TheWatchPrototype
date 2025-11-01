using Callbacks.Dialogue;
using Localization.Dialogue;
using Localization.Text;
using Managers;
using UI.Dialogue;
using UnityEngine;
using Utilities;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Dialogue Interactable")]
	public sealed class InteractableDialogueObject : Interactable
	{
		[SerializeField] private DialogueObject dialogueObject;
		[SerializeField] private TextObject questionToDisplay;
		[SerializeField] private DialogueWriterFinished dialogueWriterFinished;
		
		public override void Interact()
		{
			InputManager.Instance.ForceUIInput();
			UIManager.Instance.OpenDialogueWriter(new DialogueWriterInput(
				dialogueObject, questionToDisplay, dialogueWriterFinished));
		}
	}
}