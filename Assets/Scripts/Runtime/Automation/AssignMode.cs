using System;

namespace Runtime.Automation
{
	[Flags]
	public enum AssignMode : byte
	{
		Self = 1 << 0,
		Parent = 1 << 1,
		Child = 1 << 2,
	}
}