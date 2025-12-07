using Managers.Persistent;
using Runtime.FixedUpdate;
using UnityEngine;

namespace Agents
{
	[AddComponentMenu("Agents/Agent Rotation Handler")]
	public sealed class AgentRotationHandler : BaseBehaviour, IFixedUpdatable
	{
		[SerializeField] private AgentInputHandler inputHandler;

		public FixedUpdatePosition FixedUpdateOrder => FixedUpdatePosition.Agent;

		public void OnFixedUpdate()
		{
			if (!inputHandler.TryGetRotationAxis(out var rotationAxis)) return;
			var agentTransform = inputHandler.Rigidbody.transform;
			var eulerAngles = agentTransform.eulerAngles;
			eulerAngles.y += rotationAxis * inputHandler.RotationData.Acceleration * Mathf.Rad2Deg * Time.fixedDeltaTime;
			agentTransform.eulerAngles = eulerAngles;
		}
		
		private void Start() => GameManager.Instance.AddFixedUpdate(this);

		private void OnDestroy() => GameManager.Instance?.RemoveFixedUpdate(this);
	}
}