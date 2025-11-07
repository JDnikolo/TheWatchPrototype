using Character;
using Managers;
using Unity.Cinemachine;
using UnityEngine;
using Utilities;

namespace Player
{
    [AddComponentMenu("Player/Player Movement Handler")]
    public sealed class PlayerMovementHandler : MonoBehaviour, IFixedUpdatable
    {
        [SerializeField] private CinemachineCamera cinemachineCamera;
        [SerializeField] private PlayerInputHandler inputHandler;
        [SerializeField] private CharacterVelocityData velocityData;

        public byte UpdateOrder => 0;

        public void OnFixedUpdate()
        {
            var finalVector = Vector2.zero;
            var deltaTime = Time.fixedDeltaTime;
            var rigidBody = inputHandler.Rigidbody;
            var linearVelocity = rigidBody.velocity.ToFlatVector();
            var acceleration = velocityData.Acceleration;
            var cameraTransform = cinemachineCamera.transform;
            if (inputHandler.TryGetMoveAxis(out var moveAxis) && moveAxis != Vector2.zero)
            {
                finalVector = (cameraTransform.right * moveAxis.x + 
                               cameraTransform.forward * moveAxis.y).ToFlatVector();
                finalVector = finalVector.normalized * acceleration;
            }
            else
            {
                finalVector.CorrectForDirection(cameraTransform.forward.ToFlatVector().normalized, 
                    linearVelocity, 0f, acceleration, deltaTime);
                finalVector.CorrectForDirection(cameraTransform.right.ToFlatVector().normalized, 
                    linearVelocity, 0f, acceleration, deltaTime);
            }

            rigidBody.AddForce(finalVector.FromFlatVector(), ForceMode.Acceleration);
        }

        private void Awake() => GameManager.Instance.AddFixedUpdateSafe(this);

        private void OnDestroy() => GameManager.Instance.RemoveFixedUpdateSafe(this);

        private void OnValidate()
        {
            var rigidBody = inputHandler.Rigidbody;
            if (rigidBody && velocityData) rigidBody.maxLinearVelocity = velocityData.MaxVelocity;
        }
    }
}