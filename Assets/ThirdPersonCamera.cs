using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private float _cameraSensitivityX = Mathf.PI * 0.01f;
    [SerializeField] private float _cameraSensitivityY = Mathf.PI * 0.01f;
    [SerializeField] private float _cameraTiltLimit = 85f;


    private CinemachineCamera cinemachineCamera;
    private Vector2 _mouseInput;
    void Awake()
    {
        cinemachineCamera = GetComponentInChildren<CinemachineCamera>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

    }

    public void OnLook(InputValue value)
    {
        _mouseInput = value.Get<Vector2>();
    }
}
