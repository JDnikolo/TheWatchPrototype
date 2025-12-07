using UnityEngine;

namespace Highlighting
{
    [AddComponentMenu("Highlighting/Floating Text Highlightable")]
    public sealed class HighlightableFloatingText : Highlightable
    {
        [SerializeField] private TMPro.TextMeshPro text;
		
        protected override void HighlightInternal(bool enable) => text.enabled = enable;
    }
}