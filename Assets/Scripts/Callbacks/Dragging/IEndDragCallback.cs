using UnityEngine.EventSystems;

namespace Callbacks.Dragging
{
	public interface IEndDragCallback
	{
		void OnEndDrag(PointerEventData eventData);
	}
}