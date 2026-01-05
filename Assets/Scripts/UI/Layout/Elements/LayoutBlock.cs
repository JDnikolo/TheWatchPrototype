using System;
using UnityEngine;

namespace UI.Layout.Elements
{
	[AddComponentMenu("UI/Layout/Block")]
	public sealed class LayoutBlock : LayoutElementBase
	{
		public override ILayoutElement Parent
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override ILayoutElement LeftNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override ILayoutElement RightNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override ILayoutElement TopNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override ILayoutElement BottomNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}
		
		public override void OnInput(Vector2 axis, Direction input)
		{
		}

		public override void Select() => gameObject.SetActive(true);

		public override void Deselect() => gameObject.SetActive(false);
#if UNITY_EDITOR
		public override LayoutElementBase LeftManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override LayoutElementBase RightManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override LayoutElementBase TopManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override LayoutElementBase BottomManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}
#endif
	}
}