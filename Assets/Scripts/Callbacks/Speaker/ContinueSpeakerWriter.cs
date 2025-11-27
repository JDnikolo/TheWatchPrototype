using Interactables;
using UI.Speaker;
using UnityEngine;

namespace Callbacks.Speaker
{
	[AddComponentMenu("Callbacks/Text/Continue Speaker Writer")]
	public sealed class ContinueSpeakerWriter : SpeakerWriterFinished
	{
		[SerializeField] private Interactable interactable;
		
		public override void OnTextWriterFinished(SpeakerWriter textWriter)
		{
			if (interactable) interactable.Interact();
		}
	}
}