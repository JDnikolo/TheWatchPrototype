namespace Callbacks.Layout
{
	public interface ILayoutExplicitCallbackImplementer
	{
		bool IsExplicit { get; }
		
		void SetExplicitCallback(ILayoutExplicitCallback callback);
	}
}