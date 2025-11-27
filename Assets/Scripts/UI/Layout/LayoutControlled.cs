using System;
using Callbacks.Layout;
using Managers;
using UnityEngine;
using Utilities;

namespace UI.Layout
{
	public abstract class LayoutControlled : LayoutManaged, ILayoutInput
	{
		[SerializeField] [HideInInspector] private LayoutElement leftNeighbor;
		[SerializeField] [HideInInspector] private LayoutElement rightNeighbor;
		[SerializeField] [HideInInspector] private LayoutElement topNeighbor;
		[SerializeField] [HideInInspector] private LayoutElement bottomNeighbor;

		private ILayoutInputCallback m_inputCallback;

		public ILayoutElement LeftNeighbor { get; set; }

		public ILayoutElement RightNeighbor { get; set; }

		public ILayoutElement TopNeighbor { get; set; }

		public ILayoutElement BottomNeighbor { get; set; }
		
		public void SetControlCallback(ILayoutInputCallback inputCallback) => m_inputCallback = inputCallback;

		public void OnInput(Vector2 axis, Direction input)
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
		}

		public override void OnPrewarm()
		{
			base.OnPrewarm();
			LeftNeighbor = leftNeighbor;
			RightNeighbor = rightNeighbor;
			TopNeighbor = topNeighbor;
			BottomNeighbor = bottomNeighbor;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			SetControlCallback(null);
		}
#if UNITY_EDITOR
		public LayoutElement LeftManagedNeighbor
		{
			get => leftNeighbor;
			set => this.DirtyReplace(ref leftNeighbor, value);
		}

		public LayoutElement RightManagedNeighbor
		{
			get => rightNeighbor;
			set => this.DirtyReplace(ref rightNeighbor, value);
		}

		public LayoutElement TopManagedNeighbor
		{
			get => topNeighbor;
			set => this.DirtyReplace(ref topNeighbor, value);
		}

		public LayoutElement BottomManagedNeighbor
		{
			get => bottomNeighbor;
			set => this.DirtyReplace(ref bottomNeighbor, value);
		}
#endif
	}
}