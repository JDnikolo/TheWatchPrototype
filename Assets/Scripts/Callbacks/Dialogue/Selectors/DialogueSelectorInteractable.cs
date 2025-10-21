using AYellowpaper.SerializedCollections;
using Interactables;
using Localization.Dialogue;
using UnityEngine;

namespace Callbacks.Dialogue.Selectors
{
	[AddComponentMenu("Callbacks/Dialogue/Selectors/Interactable Selector")]
	public sealed class DialogueSelectorInteractable : DialogueSelector
	{
		[SerializeField] private SerializedDictionary<DialogueOption, Interactable> dictionary;

		public override void Evaluate(DialogueOption selectedOption)
		{
			if (selectedOption && dictionary.TryGetValue(selectedOption,
					out var interactable) && interactable) interactable.Interact();
		}
	}
}