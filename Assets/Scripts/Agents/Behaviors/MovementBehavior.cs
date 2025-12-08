using Runtime.Automation;
using UnityEngine;
using Utilities;

namespace Agents.Behaviors
{
	public abstract class MovementBehavior
#if UNITY_EDITOR
		: IEditorDisplayable
#endif
	{
		/// <summary>
		/// The flat position for movement.
		/// </summary>
		public abstract Vector2 MoveTarget { get; }
		
		/// <summary>
		/// The flat position for look-rotation.
		/// </summary>
		public abstract Vector2 RotationTarget { get; }
		
		/// <summary>
		/// How quickly the agent is to slow down.
		/// </summary>
		public abstract float SlowDownMultiplier { get; }
#if UNITY_EDITOR
		public virtual void DisplayInEditor() => SlowDownMultiplier.Display("Slowdown Multiplier");

		public void DisplayInEditor(string name) => name.DisplayIndented(DisplayInEditor);
#endif
	}
}