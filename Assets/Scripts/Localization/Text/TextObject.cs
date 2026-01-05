using Attributes;
using Managers.Persistent;
using UnityEditor;
using UnityEngine;

namespace Localization.Text
{
	[CreateAssetMenu(fileName = "Text", menuName = "Localization/Text/Generic Text")]
	public class TextObject : BaseObject
	{
		[SerializeField] [EnumArray(typeof(LanguageEnum))]
		private string[] texts;

		public string Text
		{
			get
			{
#if UNITY_EDITOR
				if (!EditorApplication.isPlaying) return texts[0];
#endif
				return texts[(int) LanguageManager.Instance.Language];
			}
		}
	}
}