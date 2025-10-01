using Localization;

namespace UI
{
	public struct TextWriterInput
	{
		public TextAsset TextToDisplay;
		public ITextWriterFinished OnTextWriterFinished;

		public static readonly TextWriterInput Empty = new();
		
		public TextWriterInput(TextAsset textToDisplay, ITextWriterFinished onTextWriterFinished)
		{
			TextToDisplay = textToDisplay;
			OnTextWriterFinished = onTextWriterFinished;
		}
	}
}