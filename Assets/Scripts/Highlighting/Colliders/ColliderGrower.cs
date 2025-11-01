namespace Highlighting.Colliders
{
	public abstract class ColliderGrower
	{
		public abstract void GrowCollider(float growFactor);
		
		public abstract void ShrinkCollider();
	}
}