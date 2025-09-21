using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonPlayerController : MonoBehaviour
{
    private Vector2 _movementDirection = new(0, 0);

    [Header("Movement")]
    [SerializeField] private float _movementSpeed = 5.0f;
    [SerializeField] private float _maximumSpeed = 10f;

    private Rigidbody _rigidbody;
    [SerializeField] private Transform _spriteTransform;
    [SerializeField] private Transform _rotationCenter;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
            var target = _rotationCenter.position;
            target.y = transform.position.y;

            var movement_forward = (target - transform.position).normalized;
            var movement_right = Quaternion.AngleAxis(90, Vector3.up) * movement_forward;

            var movement = movement_right * _movementDirection.x
                + movement_forward * _movementDirection.y;
            movement.y = 0f;

            //_rigidbody.AddForce(_movementSpeed * Time.fixedDeltaTime * movement.normalized, ForceMode.VelocityChange);
            _rigidbody.linearVelocity += _movementSpeed * Time.fixedDeltaTime * movement.normalized;
            _spriteTransform.rotation = Quaternion.LookRotation(movement);
            Debug.Log(Vector3.Distance(transform.position, _rotationCenter.position));
        }
    }

    public void OnMove(InputValue value)
    {
        _movementDirection = value.Get<Vector2>();
    }
}
