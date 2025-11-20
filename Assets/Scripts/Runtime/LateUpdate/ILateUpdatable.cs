namespace Runtime.LateUpdate
{
	public interface ILateUpdatable
	{
		/// <summary>
		/// The order in which <see cref="OnLateUpdate"/> is called relative to others.
		/// </summary>
		LateUpdatePosition LateUpdateOrder { get; }
	
		/// <summary>
		/// The actual update method.
		/// </summary>
		void OnLateUpdate();
	}
}