#if UNITY_EDITOR
namespace Runtime.Automation
{
	public interface IHierarchyChanged
	{
		void OnHierarchyChanged();
	}
}
#endif