using System;

namespace Input
{
	[Flags]
	public enum ControlMap
	{
		Player = 1 << 0,
		UI = 1 << 1,
		PersistentGame = 1 << 2,
	}
}