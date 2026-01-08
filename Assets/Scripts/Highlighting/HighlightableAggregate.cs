using UnityEngine;

namespace Highlighting
{
	[AddComponentMenu("Highlighting/Aggregate Highlightable")]
	public sealed class HighlightableAggregate : Highlightable
	{
		//[MinCount(2)] 
		[SerializeField] private Highlightable[] highlightables;
		
		protected override void HighlightInternal(bool enabled)
		{
			for (var i = 0; i < highlightables.Length; i++) highlightables[i].Highlight(enabled);
		}
	}
}