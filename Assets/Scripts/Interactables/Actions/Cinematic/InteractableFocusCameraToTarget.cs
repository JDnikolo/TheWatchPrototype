using Managers;
using Managers.Persistent;
using Unity.Cinemachine;
using UnityEngine;

namespace Interactables.Actions
{
    public class InteractableFocusCameraToTarget : Interactable
    {
        [SerializeField] private Transform lookTarget = null;
        [SerializeField] private float movementDuration = 1.0f;
        [SerializeField] private Interactable onMovementEnd = null;

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