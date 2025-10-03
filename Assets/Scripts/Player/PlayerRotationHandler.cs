using Unity.Cinemachine;
using UnityEngine;

namespace Player
{
	[AddComponentMenu(menuName: "Player/Player Rotation Handler")]
	public sealed class PlayerRotationHandler : MonoBehaviour
	{
		[SerializeField] private CinemachinePanTilt cinemachinePanTilt;
		[SerializeField] private PlayerInputHandler inputHandler;

		[Header("Rotation")]
		[SerializeField] private float cameraSensitivityX = Mathf.PI * 0.01f;
		[SerializeField] private float cameraSensitivityY = Mathf.PI * 0.01f;
		
		private void Update()
		{
			if (!inputHandler.TryGetLookAxis(out var lookAxis)) return;
			lookAxis *= Time.deltaTime;
			SetClampedValue(ref cinemachinePanTilt.PanAxis, lookAxis.x * cameraSensitivityX);
			SetClampedValue(ref cinemachinePanTilt.TiltAxis, -lookAxis.y * cameraSensitivityY);
			var eulerAngles = transform.localEulerAngles;
			eulerAngles.y = cinemachinePanTilt.PanAxis.Value;
			transform.localEulerAngles = eulerAngles;
		}

		private void SetClampedValue(ref InputAxis axis, float change) => axis.Value = axis.ClampValue(axis.Value + change);
	}
}