using Boxing;

namespace Utilities
{
	public static partial class Utils
	{
		public static ImmutableRef<T> MakeImmutableRef<T>(this T value) where T : struct => value;
		
		public static MutableRef<T> MutableRefRef<T>(this T value) where T : struct => value;
	}
}