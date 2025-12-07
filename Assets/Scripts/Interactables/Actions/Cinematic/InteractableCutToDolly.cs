using Attributes;
using Managers;
using Unity.Cinemachine;
using UnityEngine;

namespace Interactables.Actions.Cinematic
{
    public sealed class InteractableCutToDolly : Interactable
    {
        [CanBeNullInPrefab, SerializeField] private CinemachineCamera dollyCamera;
        [CanBeNullInPrefab, SerializeField] private CinemachineSplineCart dollyCart;
        
        [Header("Optional")] 
        // ReSharper disable once MissingLinebreak
        [SerializeField] private float cartProgress;
        [CanBeNull, SerializeField] private Transform lookTarget;
        [SerializeField] private float movementDuration = 1.0f;
        [CanBeNull, SerializeField] private Interactable onDurationEnd;
        [SerializeField] private bool deactivateOnDurationEnd = true;

        public override void Interact() => CinematicManager.Instance.CutToDollyCamera(dollyCamera, dollyCart, 
            cartProgress, lookTarget, movementDuration, onDurationEnd, deactivateOnDurationEnd);
    }
}