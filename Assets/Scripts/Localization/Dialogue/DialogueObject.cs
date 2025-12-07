using Attributes;
using UnityEngine;

namespace Localization.Dialogue
{
	[CreateAssetMenu(fileName = "Dialogue", menuName = "Localization/Dialogue/Dialogue")]
	public class DialogueObject : BaseObject
	{
		[MinCount(1)] [SerializeField] private DialogueOption[] options;
		
		public DialogueOption[] Options => options;
	}
}