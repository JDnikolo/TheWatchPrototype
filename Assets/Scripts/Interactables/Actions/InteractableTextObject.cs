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
	[AddComponentMenu("Interactables/Text Interactable")]
	public sealed class InteractableTextObject : Interactable
	{
		[SerializeField] private SpeakerObject textObject;
		[SerializeField] private SpeakerWriterFinished textWriterFinished;
		
		public override void Interact()
		{
			InputManager.Instance.ForceUIInput();
			UIManager.Instance.OpenTextWriter(new SpeakerWriterInput(textObject, textWriterFinished));
		}
	}
}