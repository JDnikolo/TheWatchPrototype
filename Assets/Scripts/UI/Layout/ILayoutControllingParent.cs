using UnityEngine;

namespace UI.Layout
{
	public interface ILayoutControllingParent : ILayoutParent
	{
		void OnMissedInput(Vector2 axis, Direction input);
		
		ILayoutElement OnSelectNewElement(ILayoutElement oldElement, Direction input);
		
		void OnSelectingNewHierarchy(ILayoutElement newElement, ILayoutElement oldElement);
		
#if UNITY_EDITOR
		DirectionFlags BlockedDirections { get; }
#endif
	}
}