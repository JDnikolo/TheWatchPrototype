using System;

namespace UI
{
	[Flags]
	public enum AxisFlags : byte
	{
		Horizontal = 1 << 0,
		Vertical = 1 << 1,
		
		None = 0,
		All = Horizontal | Vertical,
	}
}