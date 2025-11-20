using System;
using Managers;
using UnityEditor;
using UnityEngine;

namespace UI.Layout
{
	public abstract class LayoutControlled : LayoutManaged, ILayoutInput
	{
		[SerializeField] [HideInInspector] private LayoutElement leftNeighbor;
		[SerializeField] [HideInInspector] private LayoutElement rightNeighbor;
		[SerializeField] [HideInInspector] private LayoutElement topNeighbor;
		[SerializeField] [HideInInspector] private LayoutElement bottomNeighbor;

		private ILayoutInputCallback m_inputCallback;

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
					target = leftNeighbor;
					break;
				case Direction.Right:
					target = rightNeighbor;
					break;
				case Direction.Up:
					target = topNeighbor;
					break;
				case Direction.Down:
					target = bottomNeighbor;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(input), input, null);
			}
			
			if (target != null) LayoutManager.Instance.Select(target, input);
		}

		protected virtual void OnDestroy() => SetControlCallback(null);

#if UNITY_EDITOR
		public LayoutElement LeftNeighbor
		{
			get => leftNeighbor;
			set
			{
				if (leftNeighbor == value) return;
				leftNeighbor = value;
				EditorUtility.SetDirty(this);
			}
		}

		public LayoutElement RightNeighbor
		{
			get => rightNeighbor;
			set
			{
				if (rightNeighbor == value) return;
				rightNeighbor = value;
				EditorUtility.SetDirty(this);
			}
		}

		public LayoutElement TopNeighbor
		{
			get => topNeighbor;
			set
			{
				if (topNeighbor == value) return;
				topNeighbor = value;
				EditorUtility.SetDirty(this);
			}
		}

		public LayoutElement BottomNeighbor
		{
			get => bottomNeighbor;
			set
			{
				if (bottomNeighbor == value) return;
				bottomNeighbor = value;
				EditorUtility.SetDirty(this);
			}
		}
#endif
	}
}