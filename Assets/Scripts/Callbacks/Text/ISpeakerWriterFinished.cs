using UI.Speaker;
using UI.Text;

namespace Callbacks.Text
{
	public interface ISpeakerWriterFinished
	{
		void OnTextWriterFinished(SpeakerWriter textWriter);
	}
}