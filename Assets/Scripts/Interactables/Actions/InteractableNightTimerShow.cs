using Managers;
using UnityEngine;

namespace Interactables.Actions
{
    [AddComponentMenu("Interactables/Night Timer/Show Timer Interactable")]
    public sealed class InteractableShowTimer : Interactable
    {
        public override void Interact() => NightManager.Instance.ShowTimer();
    }
}