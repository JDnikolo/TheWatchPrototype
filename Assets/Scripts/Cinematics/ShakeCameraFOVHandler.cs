using Interactables;
using Unity.Cinemachine;
using UnityEngine;

namespace Cinematics
{
	internal sealed class ShakeCameraFOVHandler : CinematicMovementHandlerBase
	{
		private CinemachineCamera m_cameraToMove;
		private float m_initialFOV;
		private int m_shakeReps;
		private float m_nextStepProgress;
		private float m_progressPerStep;
		private float m_targetFOV;
		private float m_stepStartingFOV;

		public ShakeCameraFOVHandler(CinemachineCamera camera, float duration, float diffFOV, 
			int reps = 1, Interactable onShakeEnd = null)
		{
			m_cameraToMove = camera;
			m_initialFOV = m_stepStartingFOV = camera.Lens.FieldOfView;
			m_targetFOV = m_initialFOV + diffFOV;
			MovementDuration = duration;
			OnDurationEnd = onShakeEnd;
			m_progressPerStep = m_nextStepProgress = 0.9f / (reps * 2);
		}

		public override void ResolveMovement()
		{
			m_cameraToMove.Lens.FieldOfView = Mathf.Lerp(m_stepStartingFOV, m_targetFOV, Progress / m_nextStepProgress);
			if (Progress > 0.9f) m_cameraToMove.Lens.FieldOfView = m_initialFOV;
			if (!(Progress > m_nextStepProgress)) return;
			m_nextStepProgress += m_progressPerStep;
			var temp = 0 + m_targetFOV;
			m_targetFOV = m_stepStartingFOV;
			m_stepStartingFOV = temp;
		}
	}
}