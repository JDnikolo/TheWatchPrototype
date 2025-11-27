using Agents;
using Agents.Behaviors;

namespace Callbacks.Agent
{
	public interface IAgentCinematicAssigned
	{
		void OnAgentCinematicAssigned(AgentBrain agent, MovementCinematicBehavior behavior);
	}
}