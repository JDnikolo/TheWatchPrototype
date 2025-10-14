using UnityEngine;

namespace Highlighting.Colliders
{
	public sealed class CapsuleGrower : ColliderGrower
	{
		[SerializeField] private new CapsuleCollider collider;

		private float m_radius;
		
		private void Start() => m_radius = collider.radius;

		public override void GrowCollider(float growFactor) => collider.radius = m_radius * growFactor;

		public override void ShrinkCollider() => collider.radius = m_radius;
	}
}