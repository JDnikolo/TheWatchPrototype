using UnityEngine;

namespace Physics
{
	[AddComponentMenu("Physics/Simple Body")]
	public sealed class SimpleBody : MonoBehaviour
	{
		//Velocity
		private Vector3 m_linearVelocity;
		private Vector3 m_lastPosition;

		//Angular velocity
		private Vector3 m_angularVelocity;
		private Quaternion m_lastRotation;

		//Output
		public Vector3 LinearVelocity => m_linearVelocity;
		public Vector3 AngularVelocity => m_angularVelocity;

		private void FixedUpdate()
		{
			var localTransform = transform;
			var position = localTransform.position;
			var rotation = localTransform.rotation;
			var fixedDeltaTime = Time.fixedDeltaTime;
			m_linearVelocity = (position - m_lastPosition) / fixedDeltaTime;
			(rotation * Quaternion.Inverse(m_lastRotation)).ToAngleAxis(out var angle, out var axis);
			if (axis.sqrMagnitude > 0.001f)
				m_angularVelocity = axis.normalized * (angle * Mathf.Deg2Rad / Time.fixedDeltaTime);
			else m_angularVelocity = Vector3.zero;
			m_lastPosition = position;
			m_lastRotation = rotation;
		}
	}
}