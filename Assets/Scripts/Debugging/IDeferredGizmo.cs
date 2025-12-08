#if UNITY_EDITOR
namespace Debugging
{
	public interface IDeferredGizmo
	{
		void OnDrawGizmosDeferred();
	}
}
#endif