using UnityEngine;

namespace Highlighting.Colliders
{
	public abstract class ColliderGrower : MonoBehaviour
	{
		public abstract void GrowCollider(float growFactor);
		
		public abstract void ShrinkCollider();
	}
}