﻿using Interactables;
using Managers;
using UI.Dialogue;
using UnityEngine;
using Utilities;

namespace Callbacks.Dialogue
{
	[AddComponentMenu("Callbacks/Dialogue/Close Dialogue Writer")]
	public sealed class CloseDialogueWriter : DialogueWriterFinishedSelector
	{
		[SerializeField] private Interactable interactable;
		[SerializeField] private bool enablePlayerInput;
		
		public override void OnDialogueWriterFinsished(DialogueWriter dialogueWriter)
		{
			base.OnDialogueWriterFinsished(dialogueWriter);
			UIManager.Instance.CloseDialogueWriter();
			if (enablePlayerInput) InputManager.Instance.ForcePlayerInput();
			if (interactable) interactable.Interact();
		}
	}
}