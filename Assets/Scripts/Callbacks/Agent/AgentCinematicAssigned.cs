using Agents;
using Agents.Behaviors;

namespace Callbacks.Agent
{
	public abstract class AgentCinematicAssigned : BaseBehaviour, IAgentCinematicAssigned
	{
		public abstract void OnAgentCinematicAssigned(AgentBrain agent, MovementCinematicBehavior behavior);
	}
}