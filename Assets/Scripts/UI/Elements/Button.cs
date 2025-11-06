using Interactables;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Elements
{
	[AddComponentMenu("UI/Elements/Button")]
	public sealed partial class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
		IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
	{
		[SerializeField] private Image image;
		[SerializeField] private Color enabledColor;
		[SerializeField] private Color disabledColor;
		[SerializeField] private Color selectedColor;
		[SerializeField] private Color pressedColor;

		[SerializeField] private bool anyClick;
		[SerializeField] private Interactable onPrimaryClick;
		[SerializeField] [HideInInspector] private Interactable onSecondaryClick;

		//This gets shelved for later
		//[SerializeField] private ButtonClick[] buttonClicks;
		//private Dictionary<int, ButtonInteractions> m_onSpecificClicks;
		//private ButtonInteractions m_onAnyClick;
		private bool m_mouseOver;
		
		public void OnPointerEnter(PointerEventData eventData)
		{
			image.color = selectedColor;
			m_mouseOver = true;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			m_mouseOver = false;
			image.color = enabledColor;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (!m_mouseOver) return;
			image.color = pressedColor;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (!m_mouseOver) return;
			image.color = selectedColor;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!m_mouseOver) return;
			var clicks = eventData.clickCount;
			if (clicks == 0) return;
			var interactable = onPrimaryClick;
			if (!anyClick && clicks != 1) interactable = onSecondaryClick;
			if (interactable) interactable.Interact();
			//m_onAnyClick.Interact();
			//if (m_onSpecificClicks.TryGetValue(clicks, out var interactions)) interactions.Interact();
		}

		private void OnEnable()
		{
			if (image) image.color = enabledColor;
		}

		private void OnDisable()
		{
			if (image) image.color = disabledColor;
		}

		private void OnValidate()
		{
			if (image) image.color = enabled ? enabledColor : disabledColor;
		}
	}
}