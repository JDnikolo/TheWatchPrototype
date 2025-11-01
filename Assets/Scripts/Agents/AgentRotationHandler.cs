using UnityEngine;

namespace Agents
{
	[AddComponentMenu(menuName: "Agent/Agent Rotation Handler")]
	public sealed class AgentRotationHandler : MonoBehaviour
	{
		[SerializeField] private AgentInputHandler inputHandler;

		private void FixedUpdate()
		{
			if (!inputHandler.TryGetRotationAxis(out var rotationAxis)) return;
			var agentTransform = inputHandler.Rigidbody.transform;
			var eulerAngles = agentTransform.eulerAngles;
			eulerAngles.y += rotationAxis * inputHandler.RotationData.Acceleration * Mathf.Rad2Deg * Time.fixedDeltaTime;
			agentTransform.eulerAngles = eulerAngles;
		}
	}
}