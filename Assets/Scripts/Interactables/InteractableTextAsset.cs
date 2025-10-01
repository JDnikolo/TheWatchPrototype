using Managers;
using UI;
using UnityEngine;
using TextAsset = Localization.TextAsset;

namespace Interactables
{
	public sealed class InteractableTextAsset : Interactable
	{
		[SerializeField] private TextAsset textAsset;
		[SerializeField] private TextWriterFinished textWriterFinished;
		
		public override void Interact()
		{
			InputManager.Instance.ForceUIInput();
			DialogueManager.Instance.OpenDialogue(new TextWriterInput(textAsset, textWriterFinished));
		}
	}
}