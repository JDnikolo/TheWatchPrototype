using Attributes;
using Interactables;
using Managers;
using Managers.Persistent;
using UI.Speaker;
using UnityEngine;
using Utilities;

namespace Callbacks.Speaker
{
	[AddComponentMenu("Callbacks/Text/Close Speaker Writer")]
	public sealed class CloseSpeakerWriter : SpeakerWriterFinished
	{
		[CanBeNull, SerializeField] private Interactable interactable;
		[SerializeField] private bool enablePlayerInput;
		
		public override void OnTextWriterFinished(SpeakerWriter textWriter)
		{
			UIManager.Instance.CloseTextWriter();
			if (enablePlayerInput) InputManager.Instance.ForcePlayerInput();
			if (interactable) interactable.Interact();
		}
	}
}