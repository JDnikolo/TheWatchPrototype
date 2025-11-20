using Runtime.FrameUpdate;

namespace Collections
{
	public sealed class FrameUpdateCollection : OrderedCollection<IFrameUpdatable>
	{
		protected override byte GetPriority(IFrameUpdatable item) => (byte) item.FrameUpdateOrder;
		
		protected override void Update(IFrameUpdatable item) => item.OnFrameUpdate();
	}
}