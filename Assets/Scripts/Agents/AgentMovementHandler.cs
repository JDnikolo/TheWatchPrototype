using UnityEngine;
using Utilities;

namespace Agents
{
	[AddComponentMenu(menuName: "Agent/Agent Movement Handler")]
	public sealed class AgentMovementHandler : MonoBehaviour
	{
		[SerializeField] private AgentInputHandler inputHandler;
		[SerializeField] private new Rigidbody rigidbody;
		
		private void FixedUpdate()
		{
			var finalVector = Vector3.zero;
			var deltaTime = Time.fixedDeltaTime;
			var linearVelocity = rigidbody.velocity;
			var acceleration = inputHandler.MovementData.Acceleration;
			if (inputHandler.TryGetMoveAxis(out var moveAxis))
			{
				var moveDirection = new Vector3(moveAxis.x, 0f, moveAxis.y);
				var correctionDirection = Vector3.Cross(Vector3.up, moveDirection);
				var maxVelocity = inputHandler.MovementData.MaxVelocity;
				float multiplier;
				var length = moveAxis.z;
				var brakeDistance = maxVelocity.CalculateStoppingDistance(acceleration) *
									inputHandler.SlowdownMultiplier;
				if (length < brakeDistance) multiplier = length / brakeDistance;
				else multiplier = 1f;
				finalVector.CorrectForDirection(moveDirection, linearVelocity, 
					maxVelocity * multiplier, acceleration, deltaTime);
				finalVector.CorrectForDirection(correctionDirection, 
					linearVelocity, 0f, acceleration, deltaTime);
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