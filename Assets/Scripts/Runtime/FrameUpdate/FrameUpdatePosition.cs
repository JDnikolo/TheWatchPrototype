namespace Runtime.FrameUpdate
{
	public enum FrameUpdatePosition : byte
	{
		/// <remarks>Do not use in scripts.</remarks>
		None,
		FadeScreen,
		InputManager,
		PauseManager,
		LayoutManager,
		Player,
		Agent,
		Default,
		HighlightManager,
		GameUI,
		/// <remarks>Do not use in scripts.</remarks>
		All,
	}
}