using System;

namespace Attributes
{
	public class CanBeNullInPath : Attribute
	{
		public readonly string Target;

		public CanBeNullInPath(string target) => Target = target;

		public bool AllowNull(string path) => path.Contains(Target);
	}
}