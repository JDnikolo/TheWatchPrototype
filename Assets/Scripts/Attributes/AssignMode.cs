using System;

namespace Attributes
{
	[Flags]
	public enum AssignMode : byte
	{
		Self = 1 << 0,
		Parent = 1 << 1,
		Child = 1 << 2,
		Forced = 1 << 3,
	}
}