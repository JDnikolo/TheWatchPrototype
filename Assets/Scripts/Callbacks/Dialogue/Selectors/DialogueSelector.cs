using Localization.Dialogue;

namespace Callbacks.Dialogue.Selectors
{
	public abstract class DialogueSelector : BaseBehaviour, IDialogueSelector
	{
		public abstract void Evaluate(DialogueOption selectedOption);
	}
}