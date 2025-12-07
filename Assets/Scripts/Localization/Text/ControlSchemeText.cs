using Attributes;
using Input;
using UnityEngine;

namespace Localization.Text
{
	[CreateAssetMenu(fileName = "ControlSchemeText", menuName = "Localization/Text/Control Scheme Enum Text")]
	public sealed class ControlSchemeText : EnumText
	{
		[MinCount((int) ControlSchemeEnum.ENUM_LENGTH)] [SerializeField] [EnumArray(typeof(ControlSchemeEnum))]
		private TextObject[] values;

		public override TextObject[] Values => values;
	}
}