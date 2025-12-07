using Attributes;
using Callbacks.Speaker;
using Localization.Speaker;
using Managers;
using Managers.Persistent;
using UI.Speaker;
using UnityEngine;
using Utilities;

namespace Interactables.Actions.Speaker
{
	[AddComponentMenu("Interactables/Speaker/Display Speaker")]
	public sealed class InteractableSpeaker : Interactable
	{
		[SerializeField] private SpeakerObject textObject;
		[CanBeNull, SerializeField] private SpeakerWriterFinished textWriterFinished;
		
		public override void Interact()
		{
			InputManager.Instance.ForceUIInput();
			UIManager.Instance.OpenTextWriter(new SpeakerWriterInput(textObject, textWriterFinished));
		}
	}
}