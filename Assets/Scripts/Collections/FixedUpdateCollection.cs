namespace Collections
{
	public sealed class FixedUpdateCollection : OrderedCollection<IFixedUpdatable>
	{
		public void Update()
		{
			foreach (var pair in m_collections)
			foreach (var item in pair.Value)
				item.OnFixedUpdate();
		}

		protected override byte GetPriority(IFixedUpdatable item) => item.UpdateOrder;
	}
}