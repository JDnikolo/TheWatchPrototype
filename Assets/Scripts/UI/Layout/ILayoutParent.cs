namespace UI.Layout
{
	public interface ILayoutParent : ILayoutElement
	{
		ILayoutElement FirstChild { get; }
	}
}