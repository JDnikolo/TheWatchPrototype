using Attributes;
using Localization.Speaker;
using UnityEngine;

namespace Localization.Dialogue
{
	[CreateAssetMenu(fileName = "DialogueOption", menuName = "Localization/Dialogue/Dialogue option")]
	public sealed class DialogueOption : BaseObject
	{
		[SerializeField] private SpeakerObject textToDisplay;
		[CanBeNull] [SerializeField] private DialogueOptionCondition condition;
		
		public SpeakerObject TextToDisplay => textToDisplay;

		public bool Visible => Selectable || condition.ShowIfDisabled;

		public bool Selectable => !condition || condition.IsSelectable();
	}
}