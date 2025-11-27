using Attributes;
using Input;
using UnityEngine;

namespace Localization.Text
{
	[CreateAssetMenu(fileName = "ControlSchemaText", menuName = "Localization/Text/Control Schema Enum Text")]
	public sealed class ControlSchemaText : EnumText
	{
		[SerializeField] [EnumArray(typeof(ControlSchemeEnum))]
		private TextObject[] values;

		public override TextObject[] Values => values;
	}
}