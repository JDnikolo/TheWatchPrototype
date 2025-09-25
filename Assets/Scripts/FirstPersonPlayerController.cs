using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonPlayerController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;

    [Header("Movement")] [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float maximumSpeed = 10f;

    private Rigidbody m_rigidbody;
    private Vector2 m_movementDirection = new(0, 0);

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.maxLinearVelocity = maximumSpeed;
    }

    private void FixedUpdate()
    {
        if (!m_movementDirection.Equals(Vector3.zero))
        {
            var movement = cinemachineCamera.transform.right * m_movementDirection.x +
                           cinemachineCamera.transform.forward * m_movementDirection.y;
            movement.y = 0f;
            m_rigidbody.AddForce(movementSpeed * Time.fixedDeltaTime * movement.normalized, ForceMode.VelocityChange);
        }
    }

    public void OnMove(InputValue value) => m_movementDirection = value.Get<Vector2>();
}