﻿using Agents.Behaviors;
using UnityEngine;

namespace Agents.Starting
{
	[AddComponentMenu("Agent/Starting/Start Point-to-point")]
	public sealed class AgentStartPointToPoint : AgentStart
	{
		[SerializeField] private MovementPointToPointBehavior pointToPointBehavior;
		
		protected override void Start() => Brain.StartMovement(pointToPointBehavior);
	}
}