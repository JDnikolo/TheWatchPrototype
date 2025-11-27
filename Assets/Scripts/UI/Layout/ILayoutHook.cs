namespace UI.Layout
{
	public interface ILayoutHook : ILayoutElement
	{
		void OnHookInput(ILayoutElement oldElement, Direction input);
	}
}