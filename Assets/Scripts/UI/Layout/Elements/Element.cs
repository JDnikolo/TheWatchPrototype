using System;
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
		[SerializeField] [HideInInspector] private LayoutElement parent;
		[SerializeField] [HideInInspector] private LayoutElement leftNeighbor;
		[SerializeField] [HideInInspector] private LayoutElement rightNeighbor;
		[SerializeField] [HideInInspector] private LayoutElement topNeighbor;
		[SerializeField] [HideInInspector] private LayoutElement bottomNeighbor;

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

		public void SetParent(LayoutElement newParent) => this.DirtyReplaceObject(ref parent, newParent);

		protected virtual void OnValidate() => TestParent();

		public virtual void OnHierarchyChanged() => TestParent();

		private void TestParent()
		{
			if (parent && parent is ILayoutParent && !transform.IsChildOf(parent.transform)) SetParent(null);
		}
#endif
	}
}