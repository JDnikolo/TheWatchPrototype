using Callbacks.Speaker;
using Localization.Speaker;

namespace UI.Speaker
{
	public struct SpeakerWriterInput
	{
		public SpeakerObject TextToDisplay;
		public ISpeakerWriterFinished OnTextWriterFinished;

		public static readonly SpeakerWriterInput Empty = new();
		
		public SpeakerWriterInput(SpeakerObject textToDisplay, ISpeakerWriterFinished onTextWriterFinished)
		{
			TextToDisplay = textToDisplay;
			OnTextWriterFinished = onTextWriterFinished;
		}
	}
}