using UnityEngine;

namespace Localization.Dialogue
{
	[CreateAssetMenu(fileName = "Dialogue", menuName = "Localization/Dialogue/Dialogue")]
	public class DialogueObject : LocalizationAsset
	{
		[SerializeField] private DialogueOption[] options;
		
		public DialogueOption[] Options => options;
	}
}