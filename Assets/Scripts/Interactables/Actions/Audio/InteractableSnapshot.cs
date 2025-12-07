using Audio;
using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions.Audio
{
    [AddComponentMenu("Interactables/Audio/Transition Snapshot")]
    public sealed class InteractableSnapshot : Interactable
    {
        [SerializeField] private AudioSnapshot target;
        [SerializeField] private float fadeInTime = -1f;
        [SerializeField] private float fadeOutTime = -1f;
        [SerializeField] private bool delayedFade;
        
        public override void Interact() => 
            AudioManager.Instance.SetSnapshot(target, delayedFade, fadeInTime, fadeOutTime);
    }
}