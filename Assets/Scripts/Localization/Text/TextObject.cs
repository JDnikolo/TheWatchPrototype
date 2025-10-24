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
        [SerializeField] private string speaker;

        public string Text => texts[(int)LanguageManager.Instance.CurrentLanguage];
        public string Speaker => speaker;
    }
}