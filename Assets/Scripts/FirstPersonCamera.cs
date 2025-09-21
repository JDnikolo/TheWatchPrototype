using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private float _cameraSensitivityX = Mathf.PI * 0.01f;
    [SerializeField] private float _cameraSensitivityY = Mathf.PI * 0.01f;
    [SerializeField] private float _cameraTiltLimit = 85f;


    private CinemachineCamera cinemachineCamera;
    private Vector2 _mouseInput;
    void Awake()
    {
        cinemachineCamera = GetComponentInChildren<CinemachineCamera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, _mouseInput.x * _cameraSensitivityX * Time.deltaTime);

        // Up/Down camera movement -
        var tilt = cinemachineCamera.transform.localEulerAngles.x - _mouseInput.y * _cameraSensitivityY * Time.deltaTime;
        tilt -= (tilt > 180) ? 360 : 0;
        tilt = Mathf.Clamp(tilt, -_cameraTiltLimit, _cameraTiltLimit);

        cinemachineCamera.transform.localEulerAngles = new Vector3(
            tilt,
            cinemachineCamera.transform.localEulerAngles.y,
            0
        );

    }

    void FixedUpdate()
    {

    }

    public void OnLook(InputValue value)
    {
        _mouseInput = value.Get<Vector2>();
    }
}
