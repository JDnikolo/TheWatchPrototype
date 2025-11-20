namespace Runtime.FixedUpdate
{
	public interface IFixedUpdatable
	{
		/// <summary>
		/// The order in which <see cref="OnFixedUpdate"/> is called relative to others.
		/// </summary>
		FixedUpdatePosition FixedUpdateOrder { get; }

		/// <summary>
		/// The actual update method.
		/// </summary>
		void OnFixedUpdate();
	}
}