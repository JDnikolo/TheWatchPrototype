using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions.Audio
{
    [AddComponentMenu("Interactables/Audio/Stop Music")]
    public sealed class InteractableStopMusic : Interactable
    {
        [SerializeField] private float fadeOutTime = -1f;
        
        public override void Interact() => AudioManager.Instance.StopMusic(fadeOutTime);
    }
}