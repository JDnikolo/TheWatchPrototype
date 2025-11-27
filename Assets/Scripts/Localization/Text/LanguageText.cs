using Attributes;
using UnityEngine;

namespace Localization.Text
{
	[CreateAssetMenu(fileName = "LanguageText", menuName = "Localization/Text/Language Enum Text")]
	public sealed class LanguageText : EnumText
	{
		[SerializeField] [EnumArray(typeof(LanguageEnum))]
		private TextObject[] values;

		public override TextObject[] Values => values;
	}
}