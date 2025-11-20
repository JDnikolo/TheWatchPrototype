namespace UI.Layout
{
	public interface ILayoutHook
	{
		void OnHookInput(ILayoutElement oldElement, Direction input);
	}
}