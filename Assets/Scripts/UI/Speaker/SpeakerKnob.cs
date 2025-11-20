using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Speaker
{
	[AddComponentMenu("UI/Speaker/Speaker Knob")]
	public sealed class SpeakerKnob : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField] private SpeakerWriter writer;
		
		public void OnPointerEnter(PointerEventData eventData) => writer.DisableSkip();

		public void OnPointerExit(PointerEventData eventData) => writer.EnableSkip();
	}
}