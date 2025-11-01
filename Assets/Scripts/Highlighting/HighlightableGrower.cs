using System;
using Highlighting.Colliders;
using UnityEngine;

namespace Highlighting
{
	[AddComponentMenu("Highlighting/Growing Highlightable")]
	public sealed class HighlightableGrower : Highlightable
	{
		[SerializeField] private new Collider collider;
		[SerializeField] private float colliderGrowFactor = 1.2f;

		private ColliderGrower m_colliderGrower;

		protected override void HighlightInternal(bool enabled)
		{
			if (m_colliderGrower == null)
			{
				switch (collider)
				{
					case SphereCollider sphereCollider:
						m_colliderGrower = new SphereGrower(sphereCollider);
						break;
					case CapsuleCollider capsuleCollider:
						m_colliderGrower = new CapsuleGrower(capsuleCollider);
						break;
					default:
						throw new NotImplementedException($"{collider.GetType()} has no grower implementation.");
				}
			}
			
			else if (enabled) m_colliderGrower.GrowCollider(colliderGrowFactor);
			else m_colliderGrower.ShrinkCollider();
		}
	}
}