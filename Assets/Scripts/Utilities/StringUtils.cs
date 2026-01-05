using System.Text;
using UnityEngine.InputSystem;

namespace Utilities
{
	public static partial class Utils
	{
		public const string NULL_STRING = "null";
		
		public static string ToStringReference<T>(this T reference) where T : class => 
			reference != null ? reference.ToString() : NULL_STRING;
		
		public static string ToStringNullable<T>(this T? nullable) where T : struct =>
			nullable.HasValue ? nullable.Value.ToString() : NULL_STRING;

		public static string RemovePrefix(this string value, string prefix)
		{
			var count = prefix.Length;
			if (value.Length <= count) return string.Empty;
			return value.Substring(count);
		}
		
		public static string RemovePostfix(this string value, string postfix)
		{
			var count = postfix.Length;
			if (value.Length <= count) return string.Empty;
			return value.Substring(0, value.Length - count);
		}
		
		public static string Flush(this StringBuilder sb)
		{
			var result = sb.ToString();
			sb.Clear();
			return result;
		}
		
		public static string ToBindingDisplayString(this InputAction action, int bindingIndex) => 
			action.bindings[bindingIndex].ToDisplayString();
	}
}