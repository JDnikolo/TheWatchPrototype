#if UNITY_EDITOR
using UI.Layout;
using UI.Layout.Elements;

namespace Utilities
{
	public static partial class Utils
	{
		public static LayoutElement GetManagedParent(this LayoutElement target)
		{
			if (target is ParentBase element) return element.ManagedParent;
			return null;
		}
		
		public static void SetManagedParent(this LayoutElement target, LayoutElement parent)
		{
			if (target is ParentBase element) element.ManagedParent = parent;
		}
	}
}
#endif