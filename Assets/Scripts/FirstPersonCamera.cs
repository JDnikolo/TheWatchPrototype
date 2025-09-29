using Managers;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
	[SerializeField] private CinemachinePanTilt cinemachinePanTilt;
	[SerializeField] private string lookAxisName = "Look";

	[Header("Rotation")]
	[SerializeField] private float cameraSensitivityX = Mathf.PI * 0.01f;
	[SerializeField] private float cameraSensitivityY = Mathf.PI * 0.01f;

	private InputAction m_lookAction;

	private void Start() => m_lookAction = InputManager.Instance.GetPlayerAction(lookAxisName);

	// Update is called once per frame
	private void Update()
	{
		var mouseInput = m_lookAction.ReadValue<Vector2>();
		var deltaAxis = mouseInput * Time.deltaTime;
		SetClampedValue(ref cinemachinePanTilt.PanAxis, deltaAxis.x * cameraSensitivityX);
		SetClampedValue(ref cinemachinePanTilt.TiltAxis, -deltaAxis.y * cameraSensitivityY);
		var eulerAngles = transform.localEulerAngles;
		eulerAngles.y = cinemachinePanTilt.PanAxis.Value;
		transform.localEulerAngles = eulerAngles;
	}

	private void SetClampedValue(ref InputAxis axis, float change) => axis.Value = axis.ClampValue(axis.Value + change);
}