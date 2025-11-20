using Callbacks.Text;
using Localization.Speaker;
using Managers;
using Managers.Persistent;
using UI.Speaker;
using UnityEngine;
using Utilities;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Speaker Interactable")]
	public sealed class InteractableSpeakerObject : Interactable
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