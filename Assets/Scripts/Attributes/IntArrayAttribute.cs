using System;
using UnityEngine;

namespace Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class IntArrayAttribute : PropertyAttribute
	{
		public readonly string Format;

		public IntArrayAttribute(string format) => Format = format;
	}
}