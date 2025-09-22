using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private float cameraSensitivityX = Mathf.PI * 0.01f;
    [SerializeField] private float cameraSensitivityY = Mathf.PI * 0.01f;
    [SerializeField] private float cameraTiltLimit = 85f;


    private CinemachineCamera m_cinemachineCamera;
    private Vector2 m_mouseInput;

    private void Awake() => m_cinemachineCamera = GetComponentInChildren<CinemachineCamera>();

    public void OnLook(InputValue value) => m_mouseInput = value.Get<Vector2>();
}
