using UnityEngine;

namespace UI
{
	public abstract class TextWriterFinished : MonoBehaviour, ITextWriterFinished
	{
		public abstract void OnTextWriterFinished(TextWriter textWriter);
	}
}