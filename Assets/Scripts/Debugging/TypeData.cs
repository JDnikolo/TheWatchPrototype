#if UNITY_EDITOR
namespace Debugging
{
	public enum TypeData : byte
	{
		NotTestable,
		UnityObject,
		Array,
		List,
		Dictionary,
		Serialized
	}
}
#endif