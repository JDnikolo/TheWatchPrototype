using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Text
{
	[AddComponentMenu("UI/Text/Skip Text")]
	public sealed class SkipText : MonoBehaviour, IPointerClickHandler
	{
		public void OnPointerClick(PointerEventData eventData) => UIManager.Instance.SkipText();
	}
}