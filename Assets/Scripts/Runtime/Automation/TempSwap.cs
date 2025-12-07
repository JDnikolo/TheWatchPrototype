using System;

namespace Runtime.Automation
{
	public struct TempSwap<T> : IDisposable where T : struct
	{
		public void Dispose()
		{
		}
	}
}