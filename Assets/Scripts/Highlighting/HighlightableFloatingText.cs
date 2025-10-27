using UnityEngine;

namespace Highlighting
{
    [AddComponentMenu("Highlighting/Floating Text Highlightable")]
    public sealed class HighlightableFloatingText : Highlightable
    {
        [SerializeField] private TMPro.TextMeshPro m_text;
		
        protected override void HighlightInternal(bool enable) => m_text.enabled = enable;
    }
}