namespace Collections
{
	public sealed class FixedUpdateCollection : OrderedCollection<IFixedUpdatable>
	{
		protected override byte GetPriority(IFixedUpdatable item) => item.UpdateOrder;
		
		protected override void Update(IFixedUpdatable item) => item.OnFixedUpdate();
	}
}