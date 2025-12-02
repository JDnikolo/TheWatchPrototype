using UI.Layout;
using UI.Layout.Elements;

#if UNITY_EDITOR
namespace Utilities
{
	public static partial class Utils
	{
		public static void SetManagedParent(this LayoutElement target, LayoutElement parent)
		{
			if (target is Element element) element.SetParent(parent);
		}
	}
}
#endif