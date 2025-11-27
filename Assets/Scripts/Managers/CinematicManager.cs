using System;
using System.Collections.Generic;
using Interactables;
using Managers.Persistent;
using NUnit.Framework.Constraints;
using Runtime;
using Runtime.FixedUpdate;
using Runtime.FrameUpdate;
using Unity.Cinemachine;
using UnityEngine;
using Utilities;

namespace Managers
{
    public sealed class CinematicManager : Singleton<CinematicManager>, IFrameUpdatable
    {
        protected override bool Override { get; } = true;
        public FrameUpdatePosition FrameUpdateOrder { get; } = FrameUpdatePosition.Default;
        
        private Updatable m_updatable = new();
        
        private CinemachineCamera m_cameraToMove = null;
        private Transform m_cameraMovementTarget;
        private Vector3 m_cameraInitialTransform;
        private  CameraMovementType m_movementType;
        private float m_movementDuration;
        private float m_timer;
        private Interactable OnDurationEnd;
        private bool m_cutBack = true;
        
        public void OnFrameUpdate()
        {
            if (!m_updatable.Updating) return;
            
            m_timer += Time.deltaTime;
            ResolveMovement();
            
            if (!(m_timer >= m_movementDuration)) return;
            m_movementType = CameraMovementType.None;
            if(OnDurationEnd) OnDurationEnd.Interact();
            
            if (!m_cameraToMove) m_updatable.SetUpdating(false, this);

        }

        public void CutToDollyCamera(CinemachineCamera dollyCamera, CinemachineSplineCart dollyCart, float cartProgress = 0f,
            Transform lookTarget = null, float cutDuration = -1f, Interactable onCutEnd = null, bool deactivateOnCutEnd = true)
        {
            dollyCamera.enabled = true;
            dollyCart.enabled = true;
            dollyCamera.Follow = dollyCart.transform;
            dollyCart.SplinePosition = cartProgress;
            if (lookTarget)
            {
                dollyCamera.LookAt = lookTarget;
                m_cameraMovementTarget = lookTarget;
            }
            
            if (cutDuration < 0) return;
            
            m_cameraToMove = dollyCamera;
            m_cameraInitialTransform = dollyCamera.transform.forward;
            m_movementType = CameraMovementType.CutToDolly;
            m_timer = 0;
            m_movementDuration = cutDuration;
            OnDurationEnd = onCutEnd;
            m_cutBack = deactivateOnCutEnd;
            
            m_updatable.SetUpdating(true, this);
        }

        public void FocusCameraToTarget(CinemachineCamera camera, Transform lookTarget, float duration = -1f, Interactable onMovementEnd = null)
        {
            if (duration < 0)
            {
                camera.ForceCameraPosition(camera.transform.position,
                    Quaternion.LookRotation(lookTarget.transform.position - camera.transform.position,
                        camera.transform.up));
                return;
            }
            
            m_cameraToMove = camera;
            m_cameraMovementTarget = lookTarget;
            m_cameraInitialTransform = camera.transform.forward;
            m_movementType = CameraMovementType.FocusToTarget;
            m_timer = 0;
            m_movementDuration = duration;
            OnDurationEnd = onMovementEnd;
            
           m_updatable.SetUpdating(true, this);
        }

        private void ResolveMovement()
        {
            switch (m_movementType)
            {
                case CameraMovementType.None:
                    break;
                case CameraMovementType.FocusToTarget:
                    ContinueFocusToTarget();
                    break;
                case CameraMovementType.CutToDolly:
                    ContinueCutToDolly();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        

        private void ContinueFocusToTarget()
        {
            var targetRotationForward = m_cameraMovementTarget.position -
                                        m_cameraToMove.transform.position;
            var currentTargetRotation = Vector3.Lerp(m_cameraInitialTransform, targetRotationForward, m_timer/m_movementDuration);
            m_cameraToMove.ForceCameraPosition(m_cameraToMove.transform.position,
                Quaternion.LookRotation(currentTargetRotation,
                    m_cameraToMove.transform.up));
        }

        private void ContinueCutToDolly()
        {
            if (m_timer >= m_movementDuration)
            {
                m_cameraToMove.enabled = !m_cutBack;
            }
        }

        private enum CameraMovementType
        {
            None, FocusToTarget, CutToDolly,
        }

        protected override void Awake()
        {
            base.Awake();
            GameManager.Instance.AddFrameUpdateSafe(this);
            m_updatable.SetUpdating(false, this);
        } 
        
        protected override void OnDestroy() 
        {
            GameManager.Instance.RemoveFrameUpdateSafe(this);
            base.OnDestroy();
        } 
    }
}