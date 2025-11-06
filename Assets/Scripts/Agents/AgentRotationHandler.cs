using Managers;
using UnityEngine;
using Utilities;

namespace Agents
{
	[AddComponentMenu("Agents/Agent Rotation Handler")]
	public sealed class AgentRotationHandler : MonoBehaviour, IFixedUpdatable
	{
		[SerializeField] private AgentInputHandler inputHandler;
		
		public byte UpdateOrder => 0;

		public void OnFixedUpdate()
		{
			if (!inputHandler.TryGetRotationAxis(out var rotationAxis)) return;
			var agentTransform = inputHandler.Rigidbody.transform;
			var eulerAngles = agentTransform.eulerAngles;
			eulerAngles.y += rotationAxis * inputHandler.RotationData.Acceleration * Mathf.Rad2Deg * Time.fixedDeltaTime;
			agentTransform.eulerAngles = eulerAngles;
		}
		
		private void Awake() => GameManager.Instance.AddFixedUpdateSafe(this);

		private void OnDestroy() => GameManager.Instance.RemoveFixedUpdateSafe(this);
	}
}