using System;
using UI;
using UI.Layout;

namespace Utilities
{
	public static partial class Utils
	{
		public const string ENUM_LENGTH = nameof(ENUM_LENGTH);

		public static Direction Invert(this Direction direction)
		{
			switch (direction)
			{
				case UIConstants.Direction_None:
					return UIConstants.Direction_None;
				case Direction.Left:
					return Direction.Right;
				case Direction.Right:
					return Direction.Left;
				case Direction.Up:
					return Direction.Down;
				case Direction.Down:
					return Direction.Up;
				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}
		}

		public static Axis ToAxis(this Direction direction)
		{
			switch (direction)
			{
				case UIConstants.Direction_None:
					return UIConstants.Axis_None;
				case Direction.Left:
				case Direction.Right:
					return Axis.Horizontal;
				case Direction.Up:
				case Direction.Down:
					return Axis.Vertical;
				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}
		}
#if UNITY_EDITOR
		public static bool IsDirectionBlocked(this LayoutBlockedDirections blockedDirections, Direction direction)
		{
			switch (direction)
			{
				case UIConstants.Direction_None:
					return false;
				case Direction.Left:
					return (blockedDirections & LayoutBlockedDirections.Left) != 0;
				case Direction.Right:
					return (blockedDirections & LayoutBlockedDirections.Right) != 0;
				case Direction.Up:
					return (blockedDirections & LayoutBlockedDirections.Up) != 0;
				case Direction.Down:
					return (blockedDirections & LayoutBlockedDirections.Down) != 0;
				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}
		}
#endif
	}
}