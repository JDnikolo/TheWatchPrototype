using Attributes;
using Localization.Text;
using UnityEngine;

namespace Localization
{
	[CreateAssetMenu(fileName = "Language", menuName = "Localization/Language")]
	public class Language : ScriptableObject
	{ 
		[SerializeField] [EnumArray(typeof(TextsEnum))]
		private TextObject[] texts;
#if UNITY_EDITOR
		public TextObject[] Texts
		{
			get => texts;
			set => texts = value;
		}
#endif
		public TextObject GetText(TextsEnum textEnum)
		{
			var value = (int) textEnum;
			if (value < 0 || value >= texts.Length) return null;
			return texts[value];
		}
	}
}