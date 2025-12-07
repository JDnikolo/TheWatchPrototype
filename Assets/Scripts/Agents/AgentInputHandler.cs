using Agents.Behaviors;
using Attributes;
using Callbacks.Agent;
using Character;
using Managers.Persistent;
using Runtime.FrameUpdate;
using UnityEngine;
using Utilities;

namespace Agents
{
	[AddComponentMenu("Agents/Agent Input Handler")]
	public sealed class AgentInputHandler : BaseBehaviour, IFrameUpdatable
	{
		[CanBeNullInPrefab, SerializeField] private new Rigidbody rigidbody;
		[CanBeNullInPrefab, SerializeField] private CharacterVelocityData movementData;
		[CanBeNullInPrefab, SerializeField] private CharacterVelocityData rotationData;

		public MovementBehavior MovementBehavior
		{
			get => m_movementBehavior;
			set
			{
				m_movementBehavior = value;
				if (value is IMovementBehaviorSelected special) special.OnSelected();
			}
		}
		
		public Rigidbody Rigidbody => rigidbody;

		public CharacterVelocityData MovementData => movementData;
		
		public CharacterVelocityData RotationData => rotationData;

		public Vector2 Position => transform.position.ToFlatVector();
		
		public Vector2 Velocity => rigidbody.velocity.ToFlatVector();

		public float SlowdownMultiplier => m_movementBehavior?.SlowDownMultiplier ?? 1f;
		
		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.Agent;

		private MovementBehavior m_movementBehavior;
		private Vector3 m_moveAxis;
		private float m_rotationAxis;

		public void OnFrameUpdate()
		{
			if (m_movementBehavior == null) return;
			var agentTransform = transform;
			if (m_movementBehavior is IMovementBehaviorUpdate special) 
				special.UpdateMovement(agentTransform.position);
			var position = agentTransform.position.ToFlatVector();
			var tmVector = m_movementBehavior.MoveTarget - position;
			var tmLength = tmVector.magnitude;
			var normalVector = tmVector / tmLength;
			m_moveAxis = new Vector3(normalVector.x, normalVector.y, tmLength);
			m_rotationAxis = -Vector2.SignedAngle(agentTransform.forward.ToFlatVector(),
				m_movementBehavior.RotationTarget - position) * Mathf.Deg2Rad;
		}

		public bool TryGetMoveAxis(out Vector3 moveAxis)
		{
			if (m_movementBehavior != null)
			{
				moveAxis = m_moveAxis;
				return true;
			}
			
			moveAxis = Vector2.zero;
			return false;
		}

		public bool TryGetRotationAxis(out float rotationAxis)
		{
			if (m_movementBehavior != null)
			{
				rotationAxis = m_rotationAxis;
				return true;
			}
			
			rotationAxis = 0;
			return false;
		}
		
		private void Start() => GameManager.Instance.AddFrameUpdate(this);

		private void OnDestroy() => GameManager.Instance?.RemoveFrameUpdate(this);
#if UNITY_EDITOR
		private void OnValidate()
		{
			var rigidBody = rigidbody;
			if (rigidBody)
			{
				var data = movementData;
				if (data) rigidBody.maxLinearVelocity = data.MaxVelocity;
			}
		}
#endif
	}
}