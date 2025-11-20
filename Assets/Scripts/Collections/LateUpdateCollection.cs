using Runtime.LateUpdate;

namespace Collections
{
	public sealed class LateUpdateCollection : OrderedCollection<ILateUpdatable>
	{
		protected override byte GetPriority(ILateUpdatable item) => (byte) item.LateUpdateOrder;

		protected override void Update(ILateUpdatable item) => item.OnLateUpdate();
	}
}