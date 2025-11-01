﻿using Agents.Behaviors;
using UnityEngine;

namespace Agents
{
	[AddComponentMenu("Agent/Agent Brain")]
	public sealed class AgentBrain : MonoBehaviour
	{
		[SerializeField] private AgentInputHandler inputHandler;

		public void StartMovement(MovementBehavior behavior) => inputHandler.MovementBehavior = behavior;

		public void StopMovement()
		{
			var stopBehavior = new MovementStopBehavior();
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