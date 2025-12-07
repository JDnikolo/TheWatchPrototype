using Interactables;
using Unity.Cinemachine;

namespace Cinematics
{
	public sealed class CutToDollyHandler : CinematicMovementHandlerBase
	{
		public CinemachineCamera CameraToMove { get; }
		public bool CutBack { get; }

		public override void ResolveMovement()
		{
			if (Progress >= 1) CameraToMove.enabled = !CutBack;
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
}