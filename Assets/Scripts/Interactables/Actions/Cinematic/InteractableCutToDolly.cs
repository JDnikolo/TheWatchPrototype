using Managers;
using Unity.Cinemachine;
using UnityEngine;

namespace Interactables.Actions.Cinematic
{
    public class InteractableCutToDolly : Interactable
    {
        [SerializeReference] private CinemachineCamera dollyCamera;
        [SerializeReference] private CinemachineSplineCart dollyCart;
        [Header("Optional")] 
        
        [SerializeField] private float cartProgress;
        [SerializeField] private Transform lookTarget;
        [SerializeField] private float movementDuration = 1.0f;
        [SerializeField] private Interactable onDurationEnd;
        [SerializeField] private bool deactivateOnDurationEnd = true;

        public override void Interact() => CinematicManager.Instance.CutToDollyCamera(dollyCamera, dollyCart, 
            cartProgress, lookTarget, movementDuration, onDurationEnd, deactivateOnDurationEnd);
    }
}