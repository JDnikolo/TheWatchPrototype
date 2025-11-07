using Agents.Behaviors;
using UnityEngine;

namespace Agents.Starting
{
	[AddComponentMenu("Agents/Starting/Start Follow")]
	public sealed class AgentStartFollow : AgentStart
	{
		[SerializeField] private MovementFollowBehavior followBehavior;
		
		protected override void Start() => Brain.StartMovement(followBehavior);
	}
}