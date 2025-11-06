using Agents.Starting;
using UnityEngine;

namespace Interactables.Actions
{
    [AddComponentMenu("Interactables/Movement/Set Movement Interactable")]
    public sealed class InteractableSetMovementEnabled : Interactable
    {
        [SerializeField] private AgentStart behaviorToSet;
        [SerializeField] private bool setEnabled = true;

        public override void Interact() => behaviorToSet.enabled = setEnabled;
    }
}