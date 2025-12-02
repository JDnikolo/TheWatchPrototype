using UnityEngine;

namespace Interactables.Triggers
{
    [AddComponentMenu("Interactables/Triggers/On Shout Trigger")]
    public class InteractableShoutTrigger : InteractableTrigger
    {
        public void OnGettingShouted() => OnInteract();
    }
}