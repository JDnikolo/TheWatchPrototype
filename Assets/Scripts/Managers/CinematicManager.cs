using System.Collections.Generic;
using Cinematics;
using Interactables;
using Runtime;
using Runtime.FrameUpdate;
using Unity.Cinemachine;
using UnityEngine;

namespace Managers
{
    public sealed class CinematicManager : Singleton<CinematicManager>, IFrameUpdatable
    {
        private List<CinematicMovementHandlerBase> m_activeCinematicHandlers = new();
        private Updatable m_updatable;

        protected override bool Override => true;
        
        public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.Default;
        
        public void OnFrameUpdate()
        {
            if (!m_updatable.Updating) return;
            if (m_activeCinematicHandlers.Count == 0)
            {
                m_updatable.SetUpdating(false, this);
                return;
            }

            m_activeCinematicHandlers.RemoveAll(cinematicHandler =>
            {
                cinematicHandler.ResolveMovement();
                var hasEnded = cinematicHandler.IncreaseTimer(Time.deltaTime);
                return hasEnded;
            });
        }

        public void CutToDollyCamera(CinemachineCamera dollyCamera, CinemachineSplineCart dollyCart,
            float cartProgress = 0f,
            Transform lookTarget = null, float cutDuration = -1f, Interactable onCutEnd = null,
            bool deactivateOnCutEnd = true)
        {
            dollyCamera.enabled = true;
            dollyCart.enabled = true;
            dollyCamera.Follow = dollyCart.transform;
            dollyCart.SplinePosition = cartProgress > 0? cartProgress:dollyCart.SplinePosition;
            if (lookTarget)
            {
                dollyCamera.LookAt = lookTarget;
            }
            else
            {
                dollyCamera.LookAt = null;
            }

            if (cutDuration < 0) return;

            var newCinematic = new CutToDollyHandler(
                dollyCamera,
                cutDuration,
                onCutEnd,
                deactivateOnCutEnd
            );

            m_activeCinematicHandlers.Add(newCinematic);

            m_updatable.SetUpdating(true, this);
        }

        public void FocusCameraToTarget(CinemachineCamera camera, Transform lookTarget, float duration = -1f,
            Interactable onMovementEnd = null)
        {
            if (duration < 0)
            {
                camera.ForceCameraPosition(camera.transform.position,
                    Quaternion.LookRotation(lookTarget.transform.position - camera.transform.position,
                        camera.transform.up));
                return;
            }

            var newCinematic = new FocusToTargetHandler(
                camera,
                lookTarget,
                duration,
                onMovementEnd
            );

            m_activeCinematicHandlers.Add(newCinematic);

            m_updatable.SetUpdating(true, this);

        }

        public void ShakeCameraFOV(CinemachineCamera cameraToShake, float duration, 
            float fovReduction = 5f, int reps = 10, Interactable onMovementEnd = null)
        {
            var newCinematic = new ShakeCameraFOVHandler(cameraToShake, duration, fovReduction, reps, onMovementEnd);
            m_activeCinematicHandlers.Add(newCinematic);
            m_updatable.SetUpdating(true, this);
        }

        private void Start() => m_updatable.SetUpdating(false, this);

        protected override void OnDestroy()
        {
            base.OnDestroy();
            m_updatable.SetUpdating(false, this);
        }
    }
}