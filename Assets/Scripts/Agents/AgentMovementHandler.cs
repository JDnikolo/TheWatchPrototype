using Managers.Persistent;
using Runtime.FixedUpdate;
using UnityEngine;
using Utilities;

namespace Agents
{
	[AddComponentMenu("Agents/Agent Movement Handler")]
	public sealed class AgentMovementHandler : MonoBehaviour, IFixedUpdatable
	{
		[SerializeField] private AgentInputHandler inputHandler;

		public FixedUpdatePosition FixedUpdateOrder => FixedUpdatePosition.Agent;

		public void OnFixedUpdate()
		{
			var finalVector = Vector2.zero;
			var deltaTime = Time.fixedDeltaTime;
			var rigidBody = inputHandler.Rigidbody;
			var linearVelocity = rigidBody.velocity.ToFlatVector();
			var acceleration = inputHandler.MovementData.Acceleration;
			if (inputHandler.TryGetMoveAxis(out var moveAxis))
			{
				var maxVelocity = inputHandler.MovementData.MaxVelocity;
				float multiplier;
				var length = moveAxis.z;
				var brakeDistance = maxVelocity.CalculateStoppingDistance(acceleration) *
									inputHandler.SlowdownMultiplier;
				if (length < brakeDistance) multiplier = length / brakeDistance;
				else multiplier = 1f;
				var moveDirection = new Vector3(moveAxis.x, 0f, moveAxis.y);
				var correctionDirection = Vector3.Cross(Vector3.up, moveDirection);
				finalVector.CorrectForDirection(moveDirection.ToFlatVector().normalized, 
					linearVelocity, maxVelocity * multiplier, acceleration, deltaTime);
				finalVector.CorrectForDirection(correctionDirection.ToFlatVector().normalized, 
					linearVelocity, 0f, acceleration, deltaTime);
			}
			else
			{
				finalVector.CorrectForDirection(Vector2.right, linearVelocity, 0f, acceleration, deltaTime);
				finalVector.CorrectForDirection(Vector2.up, linearVelocity, 0f, acceleration, deltaTime);
			}

			rigidBody.AddForce(finalVector.FromFlatVector(), ForceMode.Acceleration);
		}
		
		private void Awake() => GameManager.Instance.AddFixedUpdateSafe(this);

		private void OnDestroy() => GameManager.Instance.RemoveFixedUpdateSafe(this);
	}
}