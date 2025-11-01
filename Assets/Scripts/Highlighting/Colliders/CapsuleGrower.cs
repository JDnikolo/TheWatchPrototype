using UnityEngine;

namespace Highlighting.Colliders
{
	public sealed class CapsuleGrower : ColliderGrower
	{
		private CapsuleCollider m_collider;
		private float m_radius;

		public CapsuleGrower(CapsuleCollider collider)
		{
			m_collider = collider;
			if (collider) m_radius = collider.radius;
		}

		public override void GrowCollider(float growFactor) => m_collider.radius = m_radius * growFactor;

		public override void ShrinkCollider() => m_collider.radius = m_radius;
	}
}