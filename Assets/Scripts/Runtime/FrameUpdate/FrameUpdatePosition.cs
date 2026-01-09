namespace Runtime.FrameUpdate
{
	public enum FrameUpdatePosition : byte
	{
		/// <remarks>Do not use in scripts.</remarks>
		None,
		Audio,
		FadeScreen,
		InputManager,
		ComboManager,
		LayoutManager,
		JournalManager,
		NightManager,
		Player,
		Agent,
		Default,
		HighlightManager,
		GameUI,
		/// <remarks>Do not use in scripts.</remarks>
		All,
	}
}