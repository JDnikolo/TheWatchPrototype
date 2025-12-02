using UnityEngine.EventSystems;

namespace Callbacks.Pointer
{
	public interface IPointerDownCallback
	{
		void OnPointerDown(PointerEventData eventData);
	}
}