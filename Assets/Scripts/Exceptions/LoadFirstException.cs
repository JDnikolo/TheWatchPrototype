using System;

namespace Exceptions
{
	public class LoadFirstException : Exception
	{
		public LoadFirstException() : base("Load settings first!")
		{
		}
	}
}