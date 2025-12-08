using System;

namespace Input
{
	[Flags]
	public enum ControlMapEnum
	{
		Player = 1 << 0,
		UI = 1 << 1,
		PersistentGame = 1 << 2,
	}
}