using Localization.Text;

namespace UI.Text
{
	public struct TextWriterInput
	{
		public TextObject TextToDisplay;
		public ITextWriterFinished OnTextWriterFinished;

		public static readonly TextWriterInput Empty = new();
		
		public TextWriterInput(TextObject textToDisplay, ITextWriterFinished onTextWriterFinished)
		{
			TextToDisplay = textToDisplay;
			OnTextWriterFinished = onTextWriterFinished;
		}
	}
}