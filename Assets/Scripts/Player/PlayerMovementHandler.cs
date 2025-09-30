using Unity.Cinemachine;
using UnityEngine;

namespace Player
{
    public sealed class PlayerMovementHandler : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera cinemachineCamera;
        [SerializeField] private PlayerInputHandler inputHandler;
        [SerializeField] private new Rigidbody rigidbody;

        [Header("Movement")]
        [SerializeField] private float movementSpeed = 5.0f;
        [SerializeField] private float maximumSpeed = 10f;

        private void Start() => rigidbody.maxLinearVelocity = maximumSpeed;

        private void FixedUpdate()
        {
            if (!inputHandler.TryGetMoveAxis(out var moveAxis)) return;
            var movement = cinemachineCamera.transform.right * moveAxis.x +
                           cinemachineCamera.transform.forward * moveAxis.y;
            movement.y = 0f;
            rigidbody.AddForce(movementSpeed * Time.fixedDeltaTime * movement.normalized, ForceMode.VelocityChange);
        }
    }
}