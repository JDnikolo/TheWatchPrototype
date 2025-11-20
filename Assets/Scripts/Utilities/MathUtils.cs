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
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 ToFlatVector(this Vector3 position) => new(position.x, position.z);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 FromFlatVector(this Vector2 position) => new(position.x, 0f, position.y);
		
		public static void CorrectForDirection(this ref Vector2 finalVector, Vector2 direction, 
			Vector2 linearVelocity, float desiredSpeed, float acceleration, float deltaTime)
		{
			var directionSpeed = Vector2.Dot(direction, linearVelocity);
			bool towards;
			//We want to accelerate towards the direction
			if (desiredSpeed > directionSpeed) towards = true;
			//We want to accelerate against the direction
			else if (desiredSpeed < directionSpeed) towards = false;
			//No speed difference, so no change
			else return;
			
			var absError = Mathf.Abs(Mathf.Abs(directionSpeed) - Mathf.Abs(desiredSpeed));
			//If the error correction is smaller than the acceleration, we slow it down by that much
			if (absError < acceleration * deltaTime) acceleration = absError / deltaTime;
			//Then we apply the final sign with the full acceleration
			if (towards) finalVector += direction * acceleration;
			else finalVector -= direction * acceleration;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float CalculateStoppingDistance(this float velocity, float deceleration) =>
			(velocity * velocity) / (2 * deceleration);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Invert(this Vector2 vector) => new(-vector.x, -vector.y);
	}
}