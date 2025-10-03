using UnityEngine;

namespace UI.Text
{
	public abstract class TextWriterFinished : MonoBehaviour, ITextWriterFinished
	{
		public abstract void OnTextWriterFinished(TextWriter textWriter);
	}
}