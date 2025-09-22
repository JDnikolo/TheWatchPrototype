using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonPlayerController : MonoBehaviour
{
    private Vector2 _movementDirection = new(0, 0);

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float maximumSpeed = 10f;

    private Rigidbody m_rigidbody;
    private CinemachineCamera m_camera;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_camera = GetComponentInChildren<CinemachineCamera>();
        m_rigidbody.maxLinearVelocity = maximumSpeed;
    }

    private void FixedUpdate()
    {
        if (!_movementDirection.Equals(Vector3.zero))
        {
            var movement = m_camera.transform.right * _movementDirection.x + m_camera.transform.forward * _movementDirection.y;
            movement.y = 0f;
            m_rigidbody.AddForce(movementSpeed * Time.fixedDeltaTime * movement.normalized, ForceMode.VelocityChange);
        }
    }

    public void OnMove(InputValue value) => _movementDirection = value.Get<Vector2>();
}
