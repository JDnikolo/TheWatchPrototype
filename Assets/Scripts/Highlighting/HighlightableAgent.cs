using Agents;
using UnityEngine;

namespace Highlighting
{
	[AddComponentMenu("Highlighting/Agent Highlightable")]
	public sealed class HighlightableAgent : Highlightable
	{
		[SerializeField] private AgentBrain brain;
		
		protected override void HighlightInternal(bool enabled)
		{
			if (enabled) brain.StopMovement();
			else brain.RestartMovement();
		}
	}
}