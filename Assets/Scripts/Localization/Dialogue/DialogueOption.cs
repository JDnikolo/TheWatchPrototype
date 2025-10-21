using Localization.Text;
using UnityEngine;

namespace Localization.Dialogue
{
	[CreateAssetMenu(fileName = "DialogueOption", menuName = "Localization/Dialogue/Dialogue option")]
	public class DialogueOption : ScriptableObject
	{
		[SerializeField] private TextObject textToDisplay;
		[SerializeField] private DialogueOptionCondition condition;
		
		public TextObject TextToDisplay => textToDisplay;

		public bool Visible => Selectable || condition.ShowIfDisabled;

		public bool Selectable => !condition || condition.IsSelectable();
	}
}