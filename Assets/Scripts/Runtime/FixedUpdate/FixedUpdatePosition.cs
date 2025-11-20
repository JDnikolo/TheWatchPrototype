namespace Runtime.FixedUpdate
{
	public enum FixedUpdatePosition : byte
	{
		/// <remarks>Do not use in scripts.</remarks>
		None,
		PhysicsManager,
		Player,
		Agent,
		Default,
		HighlightManager,
		Animation,
		/// <remarks>Do not use in scripts.</remarks>
		All,
	}
}