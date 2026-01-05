namespace Input
{
	public struct InputBindingPair
	{
		public int Primary;
		public int Secondary;

		public bool HasSecondary => Secondary >= 0;
		
		public InputBindingPair(int primary)
		{
			Primary = primary;
			Secondary = -1;
		}

		public InputBindingPair(int primary, int secondary)
		{
			Primary = primary;
			Secondary = secondary;
		}
	}
}