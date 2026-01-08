using System;
using UI;
using UI.Layout.Elements;
using LayoutElement = UI.Layout.ILayoutElement;
using LayoutList = System.Collections.Generic.List<UI.Layout.ILayoutElement>;
using LayoutGrid = System.Collections.Generic.List<System.Collections.Generic.List<UI.Layout.ILayoutElement>>;

namespace Utilities
{
	public static partial class Utils
	{
#if UNITY_EDITOR
		public static LayoutElementBase GetManagedNeighbor(this LayoutElementBase element, Direction direction)
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

		public static void SetManagedNeighbor(this LayoutElementBase element, LayoutElementBase neighbor,
			Direction direction)
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

		public static LayoutElement GetElement(this LayoutGrid elements, Axis axis, int x, int y)
		{
			LayoutList list;
			LayoutElement element;
			switch (axis)
			{
				case Axis.Horizontal:
					return elements.TryGetValue(x, out list) && list.TryGetValue(y, out element) ? element : null;
				case Axis.Vertical:
					return elements.TryGetValue(y, out list) && list.TryGetValue(x, out element) ? element : null;
				default:
					throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
			}

		}
#endif
	}
}