using Agents.Behaviors;
using UnityEngine;

namespace Agents
{
	[AddComponentMenu("Agents/Agent Brain")]
	public sealed class AgentBrain : BaseBehaviour
	{
		[SerializeField] private AgentInputHandler inputHandler;

		public MovementBehavior MovementBehavior => inputHandler.MovementBehavior;
		
		public void StartMovement(MovementBehavior behavior) => inputHandler.MovementBehavior = behavior;

		public void StopMovement()
		{
			if (MovementBehavior is not MovementStopBehavior stopBehavior)
			{
				stopBehavior = new MovementStopBehavior();
			}
			
			stopBehavior.Stop(inputHandler.MovementBehavior, inputHandler.Position);
			inputHandler.MovementBehavior = stopBehavior;
		}

		public void RestartMovement()
		{
			if (inputHandler.MovementBehavior is MovementStopBehavior stopBehavior) 
				inputHandler.MovementBehavior = stopBehavior.Restart();
		}
	}
}