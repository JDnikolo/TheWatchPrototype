using Runtime.FixedUpdate;

namespace Collections
{
	public sealed class FixedUpdateCollection : OrderedCollection<IFixedUpdatable>
	{
		protected override byte GetPriority(IFixedUpdatable item) => (byte) item.FixedUpdateOrder;
		
		protected override void Update(IFixedUpdatable item) => item.OnFixedUpdate();
	}
}