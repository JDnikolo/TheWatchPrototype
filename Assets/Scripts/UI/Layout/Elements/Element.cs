using System;
using Attributes;
using Callbacks.Layout;
using Callbacks.Prewarm;
using Managers;
using Runtime.Automation;
using UnityEngine;
using Utilities;

namespace UI.Layout.Elements
{
	[AddComponentMenu("UI/Layout/Element")]
	public class Element : LayoutElement, IPrewarm
#if UNITY_EDITOR
		, IHierarchyChanged
#endif
	{
		[CanBeNull, SerializeField, HideInInspector]
		private LayoutElement parent;

		[CanBeNull, SerializeField, HideInInspector]
		private LayoutElement leftNeighbor;

		[CanBeNull, SerializeField, HideInInspector]
		private LayoutElement rightNeighbor;

		[CanBeNull, SerializeField, HideInInspector]
		private LayoutElement topNeighbor;

		[CanBeNull, SerializeField, HideInInspector]
		private LayoutElement bottomNeighbor;

		private ILayoutCallback m_callback;
		private ILayoutInputCallback m_inputCallback;

		public sealed override ILayoutElement Parent { get; set; }

		public sealed override ILayoutElement LeftNeighbor { get; set; }

		public sealed override ILayoutElement RightNeighbor { get; set; }

		public sealed override ILayoutElement TopNeighbor { get; set; }

		public sealed override ILayoutElement BottomNeighbor { get; set; }

		public void SetCallback(ILayoutCallback callback) => m_callback = callback;

		public void SetInputCallback(ILayoutInputCallback callback) => m_inputCallback = callback;

		public sealed override void Select() => m_callback?.OnSelected();

		public sealed override void Deselect() => m_callback?.OnDeselected();

		public sealed override void OnInput(Vector2 axis, Direction input)
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
					throw new ArgumentOutOfRangeException(nameof(input), input, null);
			}

			if (target != null) LayoutManager.Instance.Select(target, input);
			else if (Parent is ILayoutControllingParent controllingParent)
				controllingParent.OnMissedInput(axis, input);
		}

		public virtual void OnPrewarm()
		{
			Parent = parent;
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
		public sealed override LayoutElement LeftManagedNeighbor
		{
			get => leftNeighbor;
			set => this.DirtyReplaceObject(ref leftNeighbor, value);
		}

		public sealed override LayoutElement RightManagedNeighbor
		{
			get => rightNeighbor;
			set => this.DirtyReplaceObject(ref rightNeighbor, value);
		}

		public sealed override LayoutElement TopManagedNeighbor
		{
			get => topNeighbor;
			set => this.DirtyReplaceObject(ref topNeighbor, value);
		}

		public sealed override LayoutElement BottomManagedNeighbor
		{
			get => bottomNeighbor;
			set => this.DirtyReplaceObject(ref bottomNeighbor, value);
		}

		public LayoutElement GetParent() => parent;

		public void SetParent(LayoutElement newParent) => this.DirtyReplaceObject(ref parent, newParent);

		protected virtual void OnValidate()
		{
			if (parent && parent is ILayoutParent && !transform.IsChildOf(parent.transform)) SetParent(null);
			TestNeighbor(Direction.Left);
			TestNeighbor(Direction.Right);
			TestNeighbor(Direction.Up);
			TestNeighbor(Direction.Down);
		}

		private void TestNeighbor(Direction direction)
		{
			var neighbor = GetManagedNeighbor(this, direction);
			if (!neighbor) return;
			var managedParent = neighbor.GetManagedParent();
			direction = direction.Invert();
			var inverseNeighbor = GetManagedNeighbor(neighbor, direction);
			if (managedParent && inverseNeighbor == managedParent ||
				managedParent is ILayoutControllingParent controllingParent &&
				controllingParent.BlockedDirections.IsDirectionBlocked(direction)) return;
			if (inverseNeighbor != this) SetManagedNeighbor(neighbor, this, direction);
		}

		private static LayoutElement GetManagedNeighbor(LayoutElement element, Direction direction)
		{
			switch (direction)
			{
				case Direction.Left:
					return element.LeftManagedNeighbor;
				case Direction.Right:
					return element.RightManagedNeighbor;
				case Direction.Up:
					return element.TopManagedNeighbor;
				case Direction.Down:
					return element.BottomManagedNeighbor;
				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}
		}

		private static void SetManagedNeighbor(LayoutElement element, LayoutElement neighbor, Direction direction)
		{
			switch (direction)
			{
				case Direction.Left:
					element.LeftManagedNeighbor = neighbor;
					break;
				case Direction.Right:
					element.RightManagedNeighbor = neighbor;
					break;
				case Direction.Up:
					element.TopManagedNeighbor = neighbor;
					break;
				case Direction.Down:
					element.BottomManagedNeighbor = neighbor;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}
		}

		public void OnHierarchyChanged() => OnValidate();
#endif
	}
}