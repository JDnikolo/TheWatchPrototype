using Attributes;
using Callbacks.Layout;
using Managers;
using UnityEngine;
using Utilities;

namespace UI.Layout.Elements
{
	[AddComponentMenu("UI/Layout/Element")]
	public class LayoutElement : LayoutParentBase
	{
		[CanBeNull, SerializeField, HideInInspector]
		private LayoutElementBase leftNeighbor;

		[CanBeNull, SerializeField, HideInInspector]
		private LayoutElementBase rightNeighbor;

		[CanBeNull, SerializeField, HideInInspector]
		private LayoutElementBase topNeighbor;

		[CanBeNull, SerializeField, HideInInspector]
		private LayoutElementBase bottomNeighbor;

		private ILayoutCallback m_callback;
		private ILayoutInputCallback m_inputCallback;

		public sealed override ILayoutElement LeftNeighbor { get; set; }

		public sealed override ILayoutElement RightNeighbor { get; set; }

		public sealed override ILayoutElement TopNeighbor { get; set; }

		public sealed override ILayoutElement BottomNeighbor { get; set; }

		public void SetCallback(ILayoutCallback callback) => m_callback = callback;

		public void SetInputCallback(ILayoutInputCallback callback) => m_inputCallback = callback;

		public sealed override void Select() => m_callback?.OnSelected();

		public sealed override void Deselect() => m_callback?.OnDeselected();

		public override void OnInput(Vector2 axis, Direction input)
		{
			if (m_inputCallback != null) m_inputCallback.OnInput(axis, ref input);
			ILayoutElement target;
			switch (input)
			{
				case UIConstants.Direction_None:
					return;
				case Direction.Left:
					target = LeftNeighbor;
					break;
				case Direction.Right:
					target = RightNeighbor;
					break;
				case Direction.Up:
					target = TopNeighbor;
					break;
				case Direction.Down:
					target = BottomNeighbor;
					break;
				default:
					return;
			}

			if (target != null) LayoutManager.Instance.Select(target, input);
			else if (Parent is ILayoutControllingParent controllingParent)
				controllingParent.OnMissedInput(axis, input);
		}

		public override void OnPrewarm()
		{
			base.OnPrewarm();
			LeftNeighbor = leftNeighbor;
			RightNeighbor = rightNeighbor;
			TopNeighbor = topNeighbor;
			BottomNeighbor = bottomNeighbor;
		}

		protected virtual void OnDestroy()
		{
			SetCallback(null);
			SetInputCallback(null);
		}
#if UNITY_EDITOR
		public sealed override LayoutElementBase LeftManagedNeighbor
		{
			get => leftNeighbor;
			set => this.DirtyReplaceObject(ref leftNeighbor, value);
		}

		public sealed override LayoutElementBase RightManagedNeighbor
		{
			get => rightNeighbor;
			set => this.DirtyReplaceObject(ref rightNeighbor, value);
		}

		public sealed override LayoutElementBase TopManagedNeighbor
		{
			get => topNeighbor;
			set => this.DirtyReplaceObject(ref topNeighbor, value);
		}

		public sealed override LayoutElementBase BottomManagedNeighbor
		{
			get => bottomNeighbor;
			set => this.DirtyReplaceObject(ref bottomNeighbor, value);
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			TestNeighbor(Direction.Left);
			TestNeighbor(Direction.Right);
			TestNeighbor(Direction.Up);
			TestNeighbor(Direction.Down);
		}

		private void TestNeighbor(Direction direction)
		{
			var neighbor = this.GetManagedNeighbor(direction);
			if (!neighbor) return;
			switch (neighbor)
			{
				case ILayoutControllingParent controllingParent:
					if (controllingParent.BlockedDirections.IsDirectionBlocked(direction)) return;
					break;
			}
			
			direction = direction.Invert();
			var inverseNeighbor = neighbor.GetManagedNeighbor(direction);
			var managedParent = neighbor.GetManagedParent();
			if (managedParent && inverseNeighbor == managedParent) return;
			switch (managedParent)
			{
				case ILayoutControllingParent controllingParent:
					if (controllingParent.BlockedDirections.IsDirectionBlocked(direction)) return;
					break;
			}
			
			if (inverseNeighbor != this) neighbor.SetManagedNeighbor(this, direction);
		}
#endif
	}
}