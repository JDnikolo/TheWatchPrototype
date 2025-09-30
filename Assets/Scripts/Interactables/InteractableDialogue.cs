using Managers;
using UI;
using UnityEngine;

namespace Interactables
{
	public sealed class InteractableDialogue : Interactable
	{
		[SerializeField] private TextWriterInput textWriterInput;
		
		public override void Interact()
		{
			InputManager.Instance.ForceUIInput();
			UIManager.Instance.OpenDialogue(textWriterInput);
		}
	}
}