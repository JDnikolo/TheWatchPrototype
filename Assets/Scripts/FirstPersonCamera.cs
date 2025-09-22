using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private float cameraSensitivityX = Mathf.PI * 0.01f;
    [SerializeField] private float cameraSensitivityY = Mathf.PI * 0.01f;
    [SerializeField] private float cameraTiltLimit = 85f;
    
    private CinemachineCamera m_cinemachineCamera;
    private Vector2 m_mouseInput;

    private void Awake()
    {
        m_cinemachineCamera = GetComponentInChildren<CinemachineCamera>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.up, m_mouseInput.x * cameraSensitivityX * Time.deltaTime);
        // Up/Down camera movement -
        var tilt = m_cinemachineCamera.transform.localEulerAngles.x -
                   m_mouseInput.y * cameraSensitivityY * Time.deltaTime;
        if (tilt > 180) tilt -= 360;
        tilt = Mathf.Clamp(tilt, -cameraTiltLimit, cameraTiltLimit);
        m_cinemachineCamera.transform.localEulerAngles =
            new Vector3(tilt, m_cinemachineCamera.transform.localEulerAngles.y, 0);
    }

    public void OnLook(InputValue value) => m_mouseInput = value.Get<Vector2>();
}
