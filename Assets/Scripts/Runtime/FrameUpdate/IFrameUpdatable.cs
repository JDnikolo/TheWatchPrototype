namespace Runtime.FrameUpdate
{
	public interface IFrameUpdatable
	{
		/// <summary>
		/// The order in which <see cref="OnFrameUpdate"/> is called relative to others.
		/// </summary>
		FrameUpdatePosition FrameUpdateOrder { get; }
	
		/// <summary>
		/// The actual update method.
		/// </summary>
		void OnFrameUpdate();
	}
}