using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonPlayerController : MonoBehaviour
{
    private Vector2 m_movementDirection = new(0, 0);

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float maximumSpeed = 10f;

    private Rigidbody m_rigidbody;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private Transform rotationCenter;


    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.maxLinearVelocity = maximumSpeed;
    }

    private void FixedUpdate()
    {
        if (!m_movementDirection.Equals(Vector3.zero))
        {
            var target = rotationCenter.position;
            target.y = transform.position.y;
            var movement_forward = (target - transform.position).normalized;
            var movement_right = Quaternion.AngleAxis(90, Vector3.up) * movement_forward;
            var movement = movement_right * m_movementDirection.x + movement_forward * m_movementDirection.y;
            movement.y = 0f;
            //_rigidbody.AddForce(_movementSpeed * Time.fixedDeltaTime * movement.normalized, ForceMode.VelocityChange);
            m_rigidbody.linearVelocity += movementSpeed * Time.fixedDeltaTime * movement.normalized;
            spriteTransform.rotation = Quaternion.LookRotation(movement);
            Debug.Log(Vector3.Distance(transform.position, rotationCenter.position));
        }
    }

    public void OnMove(InputValue value) => m_movementDirection = value.Get<Vector2>();
}
