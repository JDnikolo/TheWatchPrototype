using Callbacks.Layout;
using UnityEngine;

namespace UI.Layout
{
	public interface ILayoutElement
	{
		ILayoutElement Parent { get; set; }

		ILayoutElement LeftNeighbor { get; set; }
		
		ILayoutElement RightNeighbor { get; set; }
		
		ILayoutElement TopNeighbor { get; set; }
		
		ILayoutElement BottomNeighbor { get; set; }
		
		void Select();
		
		void Deselect();
		
		void OnInput(Vector2 axis, Direction input);
#if UNITY_EDITOR
		LayoutElement LeftManagedNeighbor { get; set; }
		
		LayoutElement RightManagedNeighbor { get; set; }
		
		LayoutElement TopManagedNeighbor { get; set; }
		
		LayoutElement BottomManagedNeighbor { get; set; }
#endif
	}
}