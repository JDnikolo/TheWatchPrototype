public interface IStartable
{
	/// <summary>
	/// The order in which <see cref="OnStart"/> is called relative to others.
	/// </summary>
	byte StartOrder { get; }
	
	/// <summary>
	/// The actual starting method.
	/// </summary>
	void OnStart();
}