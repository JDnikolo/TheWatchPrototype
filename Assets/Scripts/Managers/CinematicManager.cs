using System.Collections.Generic;
using Interactables;
using Managers.Persistent;
using Runtime;
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
        
        private List<CinematicMovementHandlerBase> m_activeCinematicHandlers = new();
        
        /*
        private CinemachineCamera m_cameraToMove = null;
        private Transform m_cameraMovementTarget;
        private Vector3 m_cameraInitialTransform;
        private  CameraMovementType m_movementType;
        private float m_movementDuration;
        private float m_timer;
        private Interactable OnDurationEnd;
        private bool m_cutBack = true;
        */
        
        public void OnFrameUpdate()
        {
            if (!m_updatable.Updating) return;
            
            if(m_activeCinematicHandlers.Count == 0) 
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

        public void FocusCameraToTarget(CinemachineCamera camera, Transform lookTarget, float duration = -1f, Interactable onMovementEnd = null)
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

        public void ShakeCameraFOV(CinemachineCamera cameraToShake, float duration, float fovReduction = 5f,
            int reps = 10, Interactable onMovementEnd = null)
        {
            
            var newCinematic = new ShakeCameraFOVHandler(
                cameraToShake,
                duration,
                fovReduction,
                reps,
                onMovementEnd
            );
            
            m_activeCinematicHandlers.Add(newCinematic);
            
            m_updatable.SetUpdating(true, this);
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
    internal enum CameraMovementType
    {
        None, FocusToTarget, CutToDolly, ShakeCameraFOV
    }
    internal abstract class CinematicMovementHandlerBase
        {
            

            public abstract CameraMovementType MovementType { get; protected set; }

            public float MovementDuration { get; protected set; }
            public float MovementTimer { get; protected set; } = 0;
            public float Progress => MovementTimer/MovementDuration;
            
            public Interactable OnDurationEnd { get; protected set; }

            public bool IncreaseTimer(float deltaTime)
            {
                MovementTimer += deltaTime;
                if (MovementTimer >= MovementDuration) OnDurationEnd?.Interact();
                return MovementTimer > MovementDuration;
            }

            public abstract void ResolveMovement();
        }

    internal sealed class FocusToTargetHandler : CinematicMovementHandlerBase
    {
        

        public CinemachineCamera CameraToMove { get; private set; }
        public Transform CameraMovementTarget { get; private set; }
        public Vector3 CameraInitialTransform { get; private set; }

        public override CameraMovementType MovementType { get; protected set; } = CameraMovementType.FocusToTarget;
        
        public FocusToTargetHandler(CinemachineCamera camera, Transform lookTarget,
            float duration, Interactable onMovementEnd)
        {
            CameraToMove = camera;
            CameraMovementTarget = lookTarget;
            CameraInitialTransform = CameraToMove.transform.forward;
            OnDurationEnd = onMovementEnd;
            MovementDuration = duration;
        }
        
        public override void ResolveMovement()
        {
            var targetRotationForward = CameraMovementTarget.position -
                                        CameraToMove.transform.position;
            
            var currentTargetRotation = Vector3.Lerp(CameraInitialTransform,
                targetRotationForward, Progress);
            CameraToMove.ForceCameraPosition(CameraToMove.transform.position,
                Quaternion.LookRotation(currentTargetRotation, CameraToMove.transform.up));
        }
        
    }

    internal sealed class CutToDollyHandler : CinematicMovementHandlerBase
    {
        public CinemachineCamera CameraToMove { get; private set; }
        public override CameraMovementType MovementType { get; protected set; } = CameraMovementType.CutToDolly;
        public bool CutBack { get; private set; }
        public override void ResolveMovement()
        {
            if (Progress >= 1)
            {
                CameraToMove.enabled = !CutBack;
            }
        }
        public CutToDollyHandler(CinemachineCamera dollyCamera, 
            float cutDuration, Interactable onCutEnd, bool deactivateOnCutEnd)
        {
            CameraToMove = dollyCamera;
            MovementDuration = cutDuration;
            OnDurationEnd = onCutEnd;
            CutBack = deactivateOnCutEnd;
        }
    }

    internal sealed class ShakeCameraFOVHandler : CinematicMovementHandlerBase
    {
        private CinemachineCamera m_cameraToMove;

        private float m_initialFOV;
        
        private int m_shakeReps;

        private float m_nextStepProgress;

        private float m_progressPerStep;

        private float m_targetFOV;

        private float m_stepStartingFOV;
        
        public override CameraMovementType MovementType { get; protected set; } = CameraMovementType.ShakeCameraFOV;
        
        public ShakeCameraFOVHandler(CinemachineCamera camera, float duration, float diffFOV, int reps = 1, Interactable OnShakeEnd = null)
        {
            m_cameraToMove = camera;
            m_initialFOV = m_stepStartingFOV = camera.Lens.FieldOfView;
            m_targetFOV = m_initialFOV + diffFOV;
            MovementDuration = duration;
            OnDurationEnd = OnShakeEnd;
            m_progressPerStep = m_nextStepProgress = 0.9f / (reps * 2);
        }
        public override void ResolveMovement()
        {
            m_cameraToMove.Lens.FieldOfView = Mathf.Lerp(m_stepStartingFOV, m_targetFOV, Progress/m_nextStepProgress);
            if (!(Progress > m_nextStepProgress)) return;
            
            m_nextStepProgress += m_progressPerStep;
            var temp = 0 + m_targetFOV;
            m_targetFOV = m_stepStartingFOV;
            m_stepStartingFOV = temp;
        }
    }
}