using Attributes;
using UnityEngine;

namespace Interactables.Actions.Cinematic
{
    [AddComponentMenu("Interactables/Set Game Object Position And Rotation")]
    public sealed class InteractableSetAndRotationPosition : Interactable
    {
        [CanBeNullInPrefab, SerializeField] private Transform target;
        [CanBeNullInPrefab, SerializeField] private Transform targetPosition;

        public override void Interact() => 
            target.SetPositionAndRotation(targetPosition.position, targetPosition.rotation);
    }
}