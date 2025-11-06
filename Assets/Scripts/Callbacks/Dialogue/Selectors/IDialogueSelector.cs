using Localization.Dialogue;

namespace Callbacks.Dialogue.Selectors
{
	public interface IDialogueSelector
	{
		void Evaluate(DialogueOption selectedOption);
	}
}