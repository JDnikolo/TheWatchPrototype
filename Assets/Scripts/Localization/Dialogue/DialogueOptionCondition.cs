using UnityEngine;

namespace Localization.Dialogue
{
	public abstract class DialogueOptionCondition : ScriptableObject, IDialogueCondition
	{
		[SerializeField] private bool showIfDisabled;
		
		public bool ShowIfDisabled => showIfDisabled;
		
		public abstract bool IsSelectable();
	}
}