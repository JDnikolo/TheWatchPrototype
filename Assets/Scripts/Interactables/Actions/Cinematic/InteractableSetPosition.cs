using Attributes;
using UnityEngine;

namespace Interactables.Actions.Cinematic
{
    [AddComponentMenu("Interactables/Set Game Object Position")]
    public sealed class InteractableSetPosition : Interactable
    {
        //TODO make this a transform
        [CanBeNullInPrefab, SerializeField] private GameObject objectToMove;
        [CanBeNullInPrefab, SerializeField] private Transform targetPosition;

        public override void Interact() =>
            objectToMove.transform.SetPositionAndRotation(targetPosition.position, objectToMove.transform.rotation);
    }
}