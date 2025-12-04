using Interactables;
using Unity.Cinemachine;
using UnityEngine;

namespace Cinematics
{
	internal sealed class FocusToTargetHandler : CinematicMovementHandlerBase
	{
		public CinemachineCamera CameraToMove { get; }
		public Transform CameraMovementTarget { get; }
		public Vector3 CameraInitialTransform { get; }

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
			var targetRotationForward = CameraMovementTarget.position - CameraToMove.transform.position;
			var currentTargetRotation = Vector3.Lerp(CameraInitialTransform,
				targetRotationForward, Progress);
			CameraToMove.ForceCameraPosition(CameraToMove.transform.position,
				Quaternion.LookRotation(currentTargetRotation, CameraToMove.transform.up));
		}

	}
}