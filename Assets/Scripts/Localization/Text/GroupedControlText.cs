using Attributes;
using Input;
using UnityEngine;

namespace Localization.Text
{
	[CreateAssetMenu(fileName = "GroupedControlText", menuName = "Localization/Text/Grouped Control Enum Text")]
	public sealed class GroupedControlText : EnumText
	{
		[MinCount((int) GroupedControlEnum.ENUM_LENGTH)] [SerializeField, EnumArray(typeof(GroupedControlEnum))]
		private TextObject[] values;

		public override TextObject[] Values => values;
	}
}