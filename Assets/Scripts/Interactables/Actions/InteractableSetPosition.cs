using Managers;
using UnityEngine;

namespace Interactables.Actions
{
    [AddComponentMenu("Interactables/Movement/Set Position Interactable")]
    public sealed class InteractableSetPosition : Interactable
    {
        [SerializeField] private GameObject objectToMove;
        [SerializeField] private Transform targetPosition;
        
        public override void Interact() => objectToMove.transform.SetPositionAndRotation(targetPosition.position, objectToMove.transform.rotation);
    }
}