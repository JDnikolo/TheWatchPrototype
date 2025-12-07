using Attributes;
using Managers;
using Managers.Persistent;
using Unity.Cinemachine;
using UnityEngine;

namespace Interactables.Actions.Cinematic
{
    public sealed class InteractableFocusCameraToTarget : Interactable
    {
        [CanBeNullInPrefab, SerializeField] private Transform lookTarget;
        [SerializeField] private float movementDuration = 1.0f;
        
        [Header("Optional")] 
        // ReSharper disable once MissingLinebreak
        [CanBeNull, SerializeField] private Interactable onMovementEnd;

        public override void Interact()
        {
            if (!lookTarget) return;


            var activeCineCamera =
                CameraManager.Instance.Camera.gameObject.GetComponent<CinemachineBrain>().ActiveVirtualCamera
                    as CinemachineCamera;
            
            if (!activeCineCamera) return;

            CinematicManager.Instance.FocusCameraToTarget(activeCineCamera, lookTarget, movementDuration, onMovementEnd);
        }
    }
}