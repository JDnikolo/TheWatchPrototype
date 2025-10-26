using System.Runtime.CompilerServices;
using Agents;
using Agents.Starting;
using UnityEngine;

namespace Interactables.Actions
{
    [AddComponentMenu("Interactables/Set Movement Interactable")]
    public sealed class InteractableSetMovementEnabled : Interactable
    {
        [SerializeField] private AgentStart behaviorToSet;
        [SerializeField] private bool setEnabled = true;

        public override void Interact() => behaviorToSet.enabled = setEnabled;
    }
}