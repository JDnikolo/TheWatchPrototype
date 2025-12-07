using System;
using UnityEngine;

namespace Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class IntArray : PropertyAttribute
	{
		public readonly string Format;

		public IntArray(string format) => Format = format;
	}
}