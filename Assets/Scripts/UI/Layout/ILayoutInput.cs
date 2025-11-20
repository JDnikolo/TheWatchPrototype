using UnityEngine;

namespace UI.Layout
{
	public interface ILayoutInput
	{
#if UNITY_EDITOR
		LayoutElement LeftNeighbor { get; set; }
		LayoutElement RightNeighbor { get; set; }
		LayoutElement TopNeighbor { get; set; }
		LayoutElement BottomNeighbor { get; set; }
#endif
		void OnInput(Vector2 axis, Direction input);
	}
}