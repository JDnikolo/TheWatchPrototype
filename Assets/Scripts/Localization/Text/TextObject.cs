using UnityEngine;

namespace Localization.Text
{
    [CreateAssetMenu(fileName = "Text", menuName = "Localization/Text/Text")]
    public class TextObject : LocalizationAsset
    {
        [SerializeField] [TextArea] private string text;

        public string Text => text;
    }
}
