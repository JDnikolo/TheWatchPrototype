using UnityEngine.EventSystems;

namespace Callbacks.Pointer
{
	public interface IPointerExitCallback
	{
		void OnPointerExit(PointerEventData eventData);
	}
}