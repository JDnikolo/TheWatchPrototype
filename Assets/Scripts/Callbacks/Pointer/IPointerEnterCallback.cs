using UnityEngine.EventSystems;

namespace Callbacks.Pointer
{
	public interface IPointerEnterCallback
	{
		void OnPointerEnter(PointerEventData eventData);
	}
}