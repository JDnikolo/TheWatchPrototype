using Attributes;
using Managers;
using UnityEngine;

namespace Localization.Text
{
	[CreateAssetMenu(fileName = "Text", menuName = "Localization/Text/Text")]
	public class TextObject : LocalizationAsset
	{
		[SerializeField] [EnumArray(typeof(LanguageEnum))]
		private string[] texts;

		public string Text => texts[(int) LanguageManager.Instance.CurrentLanguage];
#if UNITY_EDITOR
		public string DefaultText => texts[0];
#endif
	}
}