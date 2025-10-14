namespace Collections
{
	public sealed class FrameUpdateCollection : OrderedCollection<IFrameUpdatable>
	{
		public void Update()
		{
			foreach (var pair in m_collections)
			foreach (var item in pair.Value)
				item.OnFrameUpdate();
		}

		protected override byte GetPriority(IFrameUpdatable item) => item.UpdateOrder;
	}
}