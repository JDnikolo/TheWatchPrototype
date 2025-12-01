

using UnityEngine;

namespace Interactables.Triggers
{
    [AddComponentMenu("Interactables/Triggers/Interactable Shout Trigger")]
    public class InteractableShoutTrigger : InteractableTrigger
    {

        public void OnGettingShouted()
        {
            OnInteract();
        }
        
    }
}