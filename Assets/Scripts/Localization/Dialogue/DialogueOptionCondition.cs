using UnityEngine;

namespace Localization.Dialogue
{
	public abstract class DialogueOptionCondition : ScriptableObject, IDialogueCondition
	{
		public abstract bool IsVisible();
	}
}