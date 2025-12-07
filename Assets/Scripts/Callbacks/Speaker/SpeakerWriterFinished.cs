using UI.Speaker;

namespace Callbacks.Speaker
{
	public abstract class SpeakerWriterFinished : BaseBehaviour, ISpeakerWriterFinished
	{
		public abstract void OnTextWriterFinished(SpeakerWriter textWriter);
	}
}