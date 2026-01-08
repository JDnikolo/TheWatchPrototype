#if UNITY_EDITOR
using UI.Layout.Elements;

namespace Utilities
{
	public static partial class Utils
	{
		public static LayoutElementBase GetManagedParent(this LayoutElementBase target)
		{
			if (target is LayoutParentBase element) return element.ManagedParent;
			return null;
		}
		
		public static void SetManagedParent(this LayoutElementBase target, LayoutElementBase parent)
		{
			if (target is LayoutParentBase element) element.ManagedParent = parent;
		}
	}
}
#endif