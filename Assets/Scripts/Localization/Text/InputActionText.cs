using Attributes;
using Input;
using UnityEngine;

namespace Localization.Text
{
	[CreateAssetMenu(fileName = "InputActionText", menuName = "Localization/Text/Input Action Enum Text")]
	public sealed class InputActionText : EnumText
	{
		[MinCount((int) FullControlEnum.ENUM_LENGTH)] [SerializeField] [EnumArray(typeof(FullControlEnum))]
		private TextObject[] values;

		public override TextObject[] Values => values;
	}
}