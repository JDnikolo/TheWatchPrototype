using System.Text;
using UnityEngine.InputSystem;

namespace Utilities
{
	public static partial class Utils
	{
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