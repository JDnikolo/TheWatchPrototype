namespace Collections
{
	public sealed class FrameUpdateCollection : OrderedCollection<IFrameUpdatable>
	{
		protected override byte GetPriority(IFrameUpdatable item) => item.UpdateOrder;
		
		protected override void Update(IFrameUpdatable item) => item.OnFrameUpdate();
	}
}