using Character;
using Managers;
using Managers.Persistent;
using Runtime.FixedUpdate;
using UnityEngine;
using Utilities;

namespace Player
{
    [AddComponentMenu("Player/Player Movement Handler")]
    public sealed class PlayerMovementHandler : BaseBehaviour, IFixedUpdatable
    {
        [SerializeField] private PlayerInputHandler inputHandler;
        [SerializeField] private CharacterVelocityData velocityData;

        public FixedUpdatePosition FixedUpdateOrder => FixedUpdatePosition.Player;

        public void OnFixedUpdate()
        {
            var finalVector = Vector2.zero;
            var deltaTime = Time.fixedDeltaTime;
            var rigidBody = inputHandler.Rigidbody;
            var linearVelocity = rigidBody.velocity.ToFlatVector();
            var maxVelocity = velocityData.MaxVelocity;
            var acceleration = velocityData.Acceleration;
            var cameraTransform = PlayerManager.Instance.PlayerCamera.transform;
            if (inputHandler.TryGetMoveAxis(out var moveAxis) && moveAxis != Vector2.zero)
            {
                finalVector.CorrectForDirection(cameraTransform.forward.ToFlatVector().normalized, 
                    linearVelocity, moveAxis.y * maxVelocity, acceleration, deltaTime);
                finalVector.CorrectForDirection(cameraTransform.right.ToFlatVector().normalized, 
                    linearVelocity, moveAxis.x * maxVelocity, acceleration, deltaTime);
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

        private void Start() => GameManager.Instance.AddFixedUpdate(this);

        private void OnDestroy() => GameManager.Instance?.RemoveFixedUpdate(this);
    }
}