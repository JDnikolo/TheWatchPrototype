using Managers;
using Managers.Persistent;
using Runtime.FixedUpdate;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interactables.Actions
{
    public class InteractableCutToDolly : Interactable
    {
        [SerializeReference] private CinemachineCamera dollyCamera;
        [SerializeReference] private CinemachineSplineCart dollyCart;
        [Header("Optional")] 
        
        [SerializeField] private float cartProgress = 0;
        [SerializeField] private Transform lookTarget = null;
        [SerializeField] private float movementDuration = 1.0f;
        [FormerlySerializedAs("onMovementEnd")] [SerializeField] private Interactable OnDurationEnd = null;
        [SerializeField] private bool deactivateOnDurationEnd = true;

        public override void Interact()
        {
            CinematicManager.Instance.CutToDollyCamera(dollyCamera, dollyCart, cartProgress, lookTarget, movementDuration, OnDurationEnd, deactivateOnDurationEnd);
        }
    }
}