#if UNITY_EDITOR
namespace Debugging
{
	public interface IDeferredGizmoSelected
	{
		void OnDrawGizmosSelectedDeferred();
	}
}
#endif