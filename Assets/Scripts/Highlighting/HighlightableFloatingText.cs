using UnityEngine;

namespace Highlighting
{
    [AddComponentMenu("Highlighting/Floating Text Highlightable")]
    public sealed class HighlightableFloatingText : Highlightable
    {
        //This is a serialized field, should be text not m_text
        [SerializeField] private TMPro.TextMeshPro m_text;
		
        protected override void HighlightInternal(bool enable) => m_text.enabled = enable;
    }
}