using Character;
using Unity.Cinemachine;
using UnityEngine;
using Utilities;

namespace Player
{
    [AddComponentMenu(menuName: "Player/Player Movement Handler")]
    public sealed class PlayerMovementHandler : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera cinemachineCamera;
        [SerializeField] private PlayerInputHandler inputHandler;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private CharacterVelocityData velocityData;
        
        private void Start() => rigidbody.maxLinearVelocity = velocityData.MaxVelocity;

        private void FixedUpdate()
        {
            var finalVector = Vector3.zero;
            var deltaTime = Time.fixedDeltaTime;
            var linearVelocity = rigidbody.velocity;
            var acceleration = velocityData.Acceleration;
            if (inputHandler.TryGetMoveAxis(out var moveAxis) && moveAxis != Vector2.zero)
            {
                finalVector = cinemachineCamera.transform.right * moveAxis.x +
                              cinemachineCamera.transform.forward * moveAxis.y;
                finalVector.y = 0f;
                finalVector = finalVector.normalized * acceleration;
            }
            else
            {
                finalVector.CorrectForDirection(Vector3.forward, linearVelocity, 0f, acceleration, deltaTime);
                finalVector.CorrectForDirection(Vector3.right, linearVelocity, 0f, acceleration, deltaTime);
            }

            rigidbody.AddForce(finalVector, ForceMode.Acceleration);
        }
    }
}