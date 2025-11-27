using System;
using Agents;
using Agents.Behaviors;
using Callbacks.Agent;
using Managers.Persistent;
using Runtime.FrameUpdate;
using UnityEngine;
using Utilities;

namespace Tests
{
	public sealed class AgentCinematicTest : AgentCinematicAssigned, IFrameUpdatable
	{
		[SerializeField] private Transform moveTarget;
		[SerializeField] private Transform lookTarget;
		
		private AgentBrain m_agent;
		private MovementCinematicBehavior m_behavior;
		
		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.Default;
		
		public override void OnAgentCinematicAssigned(AgentBrain agent, MovementCinematicBehavior behavior)
		{
			m_agent = agent;
			m_behavior = behavior;
			m_behavior.SetSlowDownMultiplier(4f);
			GameManager.Instance.AddFrameUpdateSafe(this);
		}

		public void OnFrameUpdate()
		{
			m_behavior.SetMoveTarget(moveTarget.position.ToFlatVector());
			m_behavior.SetRotationTarget(lookTarget.position.ToFlatVector());
		}

		private void OnDestroy() => GameManager.Instance.RemoveFrameUpdateSafe(this);
	}
}