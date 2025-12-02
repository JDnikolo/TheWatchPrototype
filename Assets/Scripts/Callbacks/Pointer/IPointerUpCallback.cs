using UnityEngine.EventSystems;

namespace Callbacks.Pointer
{
	public interface IPointerUpCallback
	{
		void OnPointerUp(PointerEventData eventData);
	}
}