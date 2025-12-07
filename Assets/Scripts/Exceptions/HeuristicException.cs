using System;

namespace Exceptions
{
	public class HeuristicException : Exception
	{
		public HeuristicException() : base("Heuristically unreachable state detected")
		{
		}
	}
}