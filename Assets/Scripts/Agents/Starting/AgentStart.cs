using UnityEngine;

namespace Agents.Starting
{
	public abstract class AgentStart : MonoBehaviour
	{
		[SerializeField] private AgentBrain brain;
		
		protected AgentBrain Brain => brain;
		
		protected abstract void Start();
	}
}