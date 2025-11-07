using UI.Speaker;
using UnityEngine;

namespace Callbacks.Text
{
	public abstract class SpeakerWriterFinished : MonoBehaviour, ISpeakerWriterFinished
	{
		public abstract void OnTextWriterFinished(SpeakerWriter textWriter);
	}
}