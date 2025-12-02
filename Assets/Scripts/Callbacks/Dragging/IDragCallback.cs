using UnityEngine.EventSystems;

namespace Callbacks.Dragging
{
	public interface IDragCallback
	{
		void OnDrag(PointerEventData eventData);
	}
}