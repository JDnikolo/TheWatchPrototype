using System;
using Input;
using UI;
using UnityEngine.InputSystem;

namespace Utilities
{
	public static partial class Utils
	{
		public const string ENUM_LENGTH = nameof(ENUM_LENGTH);

		public static Axis Perpendicular(this Axis axis)
		{
			switch (axis)
			{
				case Axis.Horizontal:
					return Axis.Vertical;
				case Axis.Vertical:
					return Axis.Horizontal;
				default:
					throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
			}
		}
		
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

		public static InputState GetInputState(this InputAction action)
		{
			if (action.WasPressedThisFrame()) return InputState.Pressed;
			if (action.IsPressed()) return InputState.Held;
			if (action.WasReleasedThisFrame()) return InputState.Released;
			return InputState.NotPressed;
		}
		
#if UNITY_EDITOR
		public static bool IsDirectionBlocked(this DirectionFlags blockedDirections, Direction direction)
		{
			switch (direction)
			{
				case UIConstants.Direction_None:
					return false;
				case Direction.Left:
					return (blockedDirections & DirectionFlags.Left) != 0;
				case Direction.Right:
					return (blockedDirections & DirectionFlags.Right) != 0;
				case Direction.Up:
					return (blockedDirections & DirectionFlags.Up) != 0;
				case Direction.Down:
					return (blockedDirections & DirectionFlags.Down) != 0;
				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}
		}
#endif
	}
}