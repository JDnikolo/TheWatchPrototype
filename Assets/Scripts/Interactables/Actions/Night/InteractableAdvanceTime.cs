using Managers;
using UnityEngine;

namespace Interactables.Actions.Night
{
    [AddComponentMenu("Interactables/Night/Advance Night Timer")]
    public sealed class InteractableAdvanceTime : Interactable
    {
        [SerializeField] private string interactionID = "";
        [SerializeField] private int timeToAdd = 0;
        
        public override void Interact() => NightManager.Instance.RegisterInteraction(interactionID, timeToAdd);
    }
}