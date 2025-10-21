using UI.Text;
using UnityEngine;

namespace Callbacks.Text
{
	public abstract class TextWriterFinished : MonoBehaviour, ITextWriterFinished
	{
		public abstract void OnTextWriterFinished(TextWriter textWriter);
	}
}