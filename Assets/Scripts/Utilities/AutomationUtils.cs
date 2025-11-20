using UI.Layout;

#if UNITY_EDITOR
namespace Utilities
{
	public static partial class Utils
	{
		public static void SetManagedParent(this LayoutElement element, LayoutElement parent)
		{
			if (element is LayoutManaged managed) managed.SetParent(parent);
		}
	}
}
#endif