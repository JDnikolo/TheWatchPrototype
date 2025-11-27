using Agents;
using Agents.Behaviors;
using UnityEngine;

namespace Callbacks.Agent
{
	public abstract class AgentCinematicAssigned : MonoBehaviour, IAgentCinematicAssigned
	{
		public abstract void OnAgentCinematicAssigned(AgentBrain agent, MovementCinematicBehavior behavior);
	}
}