using System;
#if UNITY_EDITOR
namespace UI.Layout
{
	[Flags]
	public enum LayoutBlockedDirections : byte
	{
		Left = 1 << 0,
		Right = 1 << 1,
		Up = 1 << 2,
		Down = 1 << 3,
		
		None = 0,
		All = Left | Right | Up | Down,
	}
}
#endif