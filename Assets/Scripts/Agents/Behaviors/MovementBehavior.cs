using UnityEngine;

namespace Agents.Behaviors
{
	public abstract class MovementBehavior
	{
		/// <summary>
		/// The flat position for movement.
		/// </summary>
		public abstract Vector2 MoveTarget { get; }
		
		/// <summary>
		/// The flat position for rotation.
		/// </summary>
		public abstract Vector2 RotationTarget { get; }
		
		/// <summary>
		/// How quickly the agent is to slow down.
		/// </summary>
		public abstract float SlowDownMultiplier { get; }
	}
}