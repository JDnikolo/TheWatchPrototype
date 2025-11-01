using System;
using UnityEngine;

namespace Highlighting.Colliders
{
	[Serializable]
	public sealed class SphereGrower : ColliderGrower
	{
		private SphereCollider m_collider;
		private float m_radius;

		public SphereGrower(SphereCollider collider)
		{
			m_collider = collider;
			if (collider) m_radius = collider.radius;
		}

		public override void GrowCollider(float growFactor) => m_collider.radius = m_radius * growFactor;

		public override void ShrinkCollider() => m_collider.radius = m_radius;
	}
}