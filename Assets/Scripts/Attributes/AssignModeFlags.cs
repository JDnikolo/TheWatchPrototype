using System;

namespace Attributes
{
	[Flags]
	public enum AssignModeFlags : byte
	{
		Self = 1 << 0,
		Parent = 1 << 1,
		Child = 1 << 2
	}
}