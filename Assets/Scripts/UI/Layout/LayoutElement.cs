using Callbacks.Layout;
using UnityEngine;

namespace UI.Layout
{
	public abstract class LayoutElement : BaseBehaviour, ILayoutElement
	{
		public abstract ILayoutElement Parent { get; set; }

		public abstract ILayoutElement LeftNeighbor { get; set; }

		public abstract ILayoutElement RightNeighbor { get; set; }

		public abstract ILayoutElement TopNeighbor { get; set; }

		public abstract ILayoutElement BottomNeighbor { get; set; }

		public abstract void Select();

		public abstract void Deselect();

		public abstract void OnInput(Vector2 axis, Direction input);
#if UNITY_EDITOR
		public abstract LayoutElement LeftManagedNeighbor { get; set; }

		public abstract LayoutElement RightManagedNeighbor { get; set; }

		public abstract LayoutElement TopManagedNeighbor { get; set; }

		public abstract LayoutElement BottomManagedNeighbor { get; set; }
#endif
	}
}