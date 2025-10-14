using UnityEngine;

namespace Highlighting
{
	[AddComponentMenu("Highlighting/Aggregate Highlightable")]
	public sealed class HighlightableAggregate : Highlightable
	{
		[SerializeField] private Highlightable[] highlightables;
		
		protected override void HighlightInternal(bool enabled)
		{
			if (highlightables == null) return;
			var length = highlightables.Length;
			if (length == 0) return;
			for (var i = 0; i < length; i++) highlightables[i].Highlight(enabled);
		}
	}
}