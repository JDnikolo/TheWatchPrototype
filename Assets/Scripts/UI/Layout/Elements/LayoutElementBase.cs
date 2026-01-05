using UnityEngine;

namespace UI.Layout.Elements
{
	public abstract class LayoutElementBase : BaseBehaviour, ILayoutElement
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
		public abstract LayoutElementBase LeftManagedNeighbor { get; set; }

		public abstract LayoutElementBase RightManagedNeighbor { get; set; }

		public abstract LayoutElementBase TopManagedNeighbor { get; set; }

		public abstract LayoutElementBase BottomManagedNeighbor { get; set; }
#endif
	}
}