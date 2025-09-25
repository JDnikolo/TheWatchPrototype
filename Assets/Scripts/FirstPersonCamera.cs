using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
	[SerializeField] private CinemachinePanTilt cinemachinePanTilt;
	[SerializeField] private float cameraSensitivityX = Mathf.PI * 0.01f;
	[SerializeField] private float cameraSensitivityY = Mathf.PI * 0.01f;

	private Vector2 m_mouseInput;

	private void Awake()
	{
		//Its fine for now but the cursor should not be controlled by this script
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	private void Update()
	{
		var deltaAxis = m_mouseInput * Time.deltaTime;
		SetClampedValue(ref cinemachinePanTilt.PanAxis, deltaAxis.x * cameraSensitivityX);
		SetClampedValue(ref cinemachinePanTilt.TiltAxis, -deltaAxis.y * cameraSensitivityY);
		var eulerAngles = transform.localEulerAngles;
		eulerAngles.y = cinemachinePanTilt.PanAxis.Value;
		transform.localEulerAngles = eulerAngles;
	}

	private void SetClampedValue(ref InputAxis axis, float change) => axis.Value = axis.ClampValue(axis.Value + change);

	public void OnLook(InputValue value) => m_mouseInput = value.Get<Vector2>();
}