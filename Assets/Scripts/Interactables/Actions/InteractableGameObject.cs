using UnityEngine;

namespace Interactables.Actions
{
    [AddComponentMenu("Interactables/Game Object Interactable")]
    public sealed class InteractableGameObject : Interactable
    {
        [SerializeField] private GameObject target;
        [SerializeField] private bool setActive;
        
        public override void Interact() => target.SetActive(setActive);
    }
}