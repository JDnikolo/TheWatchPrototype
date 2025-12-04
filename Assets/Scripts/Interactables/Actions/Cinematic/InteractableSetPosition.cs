using UnityEngine;

namespace Interactables.Actions.Cinematic
{
    [AddComponentMenu("Interactables/Set Game Object Position")]
    public sealed class InteractableSetPosition : Interactable
    {
        [SerializeField] private GameObject objectToMove;
        [SerializeField] private Transform targetPosition;
        
        public override void Interact() => objectToMove.transform.SetPositionAndRotation(targetPosition.position, objectToMove.transform.rotation);
    }
}