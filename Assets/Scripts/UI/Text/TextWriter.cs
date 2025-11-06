using UnityEngine;

namespace UI.Text
{
	public abstract class TextWriter : MonoBehaviour, ITextWriter
	{
		public abstract void WriteText(string text);
	}
}