namespace Highlighting
{
	public interface IManagedHighlightable : IHighlightable
	{
		float MinHighlightDistance { get; }

		float MaxHighlightDistance { get; }
	}
}