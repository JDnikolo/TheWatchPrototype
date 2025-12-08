using System;
using UnityEngine;
using Utilities;

namespace UI.Layout.Elements
{
	[AddComponentMenu("UI/Layout/Stop Gap")]
	public sealed class StopGap : ParentBase, ILayoutParent
	{
		[SerializeField] private LayoutElement child;
		
		public ILayoutElement FirstChild => child;

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
		public override void OnHierarchyChanged()
		{
			base.OnHierarchyChanged();
			child.SetManagedParent(this);
		}

		public override LayoutElement LeftManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override LayoutElement RightManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override LayoutElement TopManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override LayoutElement BottomManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}
#endif
	}
}