using Attributes;
using Managers.Persistent;
using UnityEngine;

namespace Localization.Text
{
	[CreateAssetMenu(fileName = "Text", menuName = "Localization/Text/Generic Text")]
	public class TextObject : ScriptableObject
	{
		[SerializeField] [EnumArray(typeof(LanguageEnum))]
		private string[] texts;

		public string Text => texts[(int) LanguageManager.Instance.Language];
	}
}