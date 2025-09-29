using Managers;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonPlayerController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private string moveAxisName = "Move";

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float maximumSpeed = 10f;

    private InputAction m_moveAction;

    private void Start()
    {
        rigidbody.maxLinearVelocity = maximumSpeed;
        m_moveAction = InputManager.Instance.GetPlayerAction(moveAxisName);
    }

    private void FixedUpdate()
    {
        var movementDirection = m_moveAction.ReadValue<Vector2>();
        if (movementDirection.Equals(Vector2.zero)) return;
        var movement = cinemachineCamera.transform.right * movementDirection.x +
                       cinemachineCamera.transform.forward * movementDirection.y;
        movement.y = 0f;
        rigidbody.AddForce(movementSpeed * Time.fixedDeltaTime * movement.normalized, ForceMode.VelocityChange);
    }
}