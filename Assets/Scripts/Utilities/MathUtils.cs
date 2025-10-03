using System.Runtime.CompilerServices;
using UnityEngine;

namespace Utilities
{
	public static partial class Utils
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Squared(this float value) => value * value;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Rotate(this Vector2 vector, float radians)
		{
			//Zero angle, so no rotation in other words
			if (radians == 0f) return vector;
			return new Vector2(vector.x * Mathf.Cos(radians) - vector.y * Mathf.Sin(radians), 
				vector.x * Mathf.Sin(radians) + vector.y * Mathf.Cos(radians)
			);
		}
	}
}