using UI.Layout.Elements;
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
		LayoutElementBase LeftManagedNeighbor { get; set; }
		
		LayoutElementBase RightManagedNeighbor { get; set; }
		
		LayoutElementBase TopManagedNeighbor { get; set; }
		
		LayoutElementBase BottomManagedNeighbor { get; set; }
#endif
	}
}