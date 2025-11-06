using UnityEngine;

namespace Attributes
{
	public sealed class IntArrayAttribute : PropertyAttribute
	{
		public readonly string Format;

		public IntArrayAttribute(string format) => Format = format;
	}
}