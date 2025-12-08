using Agents;
using Attributes;
using UnityEngine;

namespace Interactables.Actions.Agent
{
	public abstract class InteractableAgent : Interactable
	{
		[SerializeField, AutoAssigned(AssignMode.Self | AssignMode.Parent, typeof(AgentBrain))]
		private AgentBrain agent;

		protected AgentBrain Agent => agent;
	}
}