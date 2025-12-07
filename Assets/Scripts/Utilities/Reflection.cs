using System;

namespace Utilities
{
	public static partial class Utils
	{
		/// <summary>
		/// Same as <see cref="Type.IsSubclassOf"/> except returns true if the types are equal
		/// </summary>
		public static bool IsDerivedOf(this Type type, Type c) =>
			type == c || type.IsSubclassOf(c) || c.IsAssignableFrom(type);
	}
}