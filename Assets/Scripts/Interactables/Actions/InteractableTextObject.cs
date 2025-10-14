using Localization.Text;
using Managers;
using UI.Text;
using UnityEngine;
using Utilities;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Text Interactable")]
	public sealed class InteractableTextObject : Interactable
	{
		[SerializeField] private TextObject textObject;
		[SerializeField] private TextWriterFinished textWriterFinished;
		
		public override void Interact()
		{
			InputManager.Instance.ForceUIInput();
			DialogueManager.Instance.OpenTextWriter(new TextWriterInput(textObject, textWriterFinished));
		}
	}
}