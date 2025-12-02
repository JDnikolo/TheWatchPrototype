using Attributes;
using Input;
using UnityEngine;

namespace Localization.Text
{
	[CreateAssetMenu(fileName = "InputActionText", menuName = "Localization/Text/Input Action Enum Text")]
	public sealed class InputActionText : EnumText
	{
		[SerializeField] [EnumArray(typeof(ControlEnum))]
		private TextObject[] values;

		public override TextObject[] Values => values;
	}
}