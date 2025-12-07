using Agents;
using UnityEngine;

namespace Interactables.Actions.Agent
{
    [AddComponentMenu("Interactables/Stop Agent")]
    public sealed class InteractableStopAgent : Interactable
    {
        [SerializeField] private AgentBrain agentBrain;
		
        public override void Interact() => agentBrain.StopMovement();
    }
}