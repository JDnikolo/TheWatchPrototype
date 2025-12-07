using System;

namespace Attributes
{
	public sealed class CustomDebug : Attribute
	{
		public readonly string MethodName;

		public CustomDebug(string methodName) => MethodName = methodName;
	}
}