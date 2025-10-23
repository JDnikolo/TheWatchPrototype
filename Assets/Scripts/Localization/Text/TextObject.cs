using Attributes;
using UnityEngine;

namespace Localization.Text
{
    [CreateAssetMenu(fileName = "Text", menuName = "Localization/Text/Text")]
    public class TextObject : LocalizationAsset
    {
        [SerializeField] [EnumArray(typeof(LanguageEnum))]
        private string[] texts;

        public string Text => null;
    }
}