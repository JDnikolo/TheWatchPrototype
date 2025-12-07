namespace UI.Text
{
	public abstract class TextWriter : BaseBehaviour, ITextWriter
	{
		public abstract void WriteText(string text);
	}
}