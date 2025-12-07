using Callbacks.Dialogue;
using UnityEngine;

namespace Localization.Dialogue
{
	public abstract class DialogueOptionCondition : BaseObject, IDialogueCondition
	{
		[SerializeField] private bool showIfDisabled;
		
		public bool ShowIfDisabled => showIfDisabled;
		
		public abstract bool IsSelectable();
	}
}