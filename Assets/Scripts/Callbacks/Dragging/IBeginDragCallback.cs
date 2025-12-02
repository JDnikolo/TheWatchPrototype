using UnityEngine.EventSystems;

namespace Callbacks.Dragging
{
	public interface IBeginDragCallback
	{
		void OnBeginDrag(PointerEventData eventData);
	}
}