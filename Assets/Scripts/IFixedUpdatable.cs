public interface IFixedUpdatable
{
	/// <summary>
	/// The order in which <see cref="OnFixedUpdate"/> is called relative to others.
	/// </summary>
	byte UpdateOrder { get; }

	/// <summary>
	/// The actual update method.
	/// </summary>
	void OnFixedUpdate();
}