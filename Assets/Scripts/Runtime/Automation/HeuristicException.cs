using System;

namespace Runtime.Automation
{
	public class HeuristicException : Exception
	{
		public HeuristicException() : base("Heuristically unreachable state detected")
		{
		}
	}
}