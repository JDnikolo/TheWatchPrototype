using Agents.Starting;
using UnityEngine;

namespace Interactables.Actions.Agent
{
    [AddComponentMenu("Interactables/Agent/Start Agent Behavior")]
    public sealed class InteractableSetMovementEnabled : Interactable
    {
        [SerializeField] private AgentStart behaviorToSet;
        [SerializeField] private bool setEnabled = true;

        public override void Interact() => behaviorToSet.enabled = setEnabled;
    }
}