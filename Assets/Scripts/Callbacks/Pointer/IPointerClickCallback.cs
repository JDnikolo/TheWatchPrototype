using UnityEngine.EventSystems;

namespace Callbacks.Pointer
{
	public interface IPointerClickCallback
	{
		void OnPointerClick(PointerEventData eventData);
	}
}