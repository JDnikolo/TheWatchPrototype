using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Speaker
{
	[AddComponentMenu("UI/Speaker/Skip Speaker")]
	public sealed class SkipSpeaker : MonoBehaviour, IPointerClickHandler
	{
		public void OnPointerClick(PointerEventData eventData) => UIManager.Instance.SkipSpeaker();
	}
}