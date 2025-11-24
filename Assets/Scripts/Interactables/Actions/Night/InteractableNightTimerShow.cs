using Managers;
using UnityEngine;

namespace Interactables.Actions.Night
{
    [AddComponentMenu("Interactables/Night/Show Night Timer")]
    public sealed class InteractableShowTimer : Interactable
    {
        public override void Interact() => NightManager.Instance.ShowTimer();
    }
}