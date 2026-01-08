namespace Input
{
	public struct InputBindingPair
	{
		public int Primary;
		public int Secondary;

		public bool HasSecondary => Secondary >= 0;

		public static InputBindingPair Invalid = new(-1, -1);

		public InputBindingPair(int primary, int secondary)
		{
			Primary = primary;
			Secondary = secondary;
		}
	}
}