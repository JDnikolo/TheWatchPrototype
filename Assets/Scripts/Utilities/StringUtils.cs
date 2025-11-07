using System.Text;

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
	}
}