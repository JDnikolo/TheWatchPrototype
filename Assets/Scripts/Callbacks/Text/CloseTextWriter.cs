using Interactables;
using Managers;
using UI.Text;
using UnityEngine;
using Utilities;

namespace Callbacks.Text
{
	[AddComponentMenu("Callbacks/Text/Close Text Writer")]
	public sealed class CloseTextWriter : TextWriterFinished
	{
		[SerializeField] private Interactable interactable;
		[SerializeField] private bool enablePlayerInput;
		
		public override void OnTextWriterFinished(TextWriter textWriter)
		{
			DialogueManager.Instance.CloseTextWriter();
			if (enablePlayerInput) InputManager.Instance.ForcePlayerInput();
			if (interactable) interactable.Interact();
		}
	}
}