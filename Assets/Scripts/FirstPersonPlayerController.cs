using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonPlayerController : MonoBehaviour
{
    private Vector2 _movementDirection = new(0, 0);

    [Header("Movement")]
    [SerializeField] private float _movementSpeed = 5.0f;
    [SerializeField] private float _maximumSpeed = 10f;

    private Rigidbody _rigidbody;
    private CinemachineCamera _camera;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = GetComponentInChildren<CinemachineCamera>();
        _rigidbody.maxLinearVelocity = _maximumSpeed;
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
        if (!_movementDirection.Equals(Vector3.zero))
        {
            var movement = _camera.transform.right * _movementDirection.x
                + _camera.transform.forward * _movementDirection.y;
            movement.y = 0f;

            _rigidbody.AddForce(_movementSpeed * Time.fixedDeltaTime * movement.normalized, ForceMode.VelocityChange);
        }
    }

    public void OnMove(InputValue value)
    {
        _movementDirection = value.Get<Vector2>();
    }
}
