using Localization.Dialogue;
using UnityEngine;

namespace Callbacks.Dialogue.Selectors
{
	public abstract class DialogueSelector : MonoBehaviour, IDialogueSelector
	{
		public abstract void Evaluate(DialogueOption selectedOption);
	}
}