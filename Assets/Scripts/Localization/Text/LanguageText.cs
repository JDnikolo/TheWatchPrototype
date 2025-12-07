using Attributes;
using UnityEngine;

namespace Localization.Text
{
	[CreateAssetMenu(fileName = "LanguageText", menuName = "Localization/Text/Language Enum Text")]
	public sealed class LanguageText : EnumText
	{
		[MinCount((int) LanguageEnum.ENUM_LENGTH)] [SerializeField] [EnumArray(typeof(LanguageEnum))]
		private TextObject[] values;

		public override TextObject[] Values => values;
	}
}