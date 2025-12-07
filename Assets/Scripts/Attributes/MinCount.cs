using System;

namespace Attributes
{
	public sealed class MinCount : Attribute
	{
		public readonly int Target;

		public MinCount(int target) => Target = target;
	}
}