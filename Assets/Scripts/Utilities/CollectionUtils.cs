using System.Collections.Generic;

namespace Utilities
{
	public static partial class Utils
	{
		public static bool ContainsKey<T>(this ICollection<T> collection, int key) => 
			key >= 0 && key < collection.Count;
	}
}