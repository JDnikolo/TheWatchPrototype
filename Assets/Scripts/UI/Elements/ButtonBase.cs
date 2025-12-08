using Attributes;
using Callbacks.Pointer;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Elements
{
	public abstract class ButtonBase : Parent, IPointerDownCallback, IPointerUpCallback, IPointerClickCallback
	{
		[SerializeField] [AutoAssigned(AssignModeFlags.Self, typeof(RectTransform))]  
		private RectTransform rectTransform;
		
		protected RectTransform RectTransform => rectTransform;
		
		private bool m_mouseOver;
		private bool m_pressed;
		
		public override void OnPointerEnter(PointerEventData eventData)
		{
			m_mouseOver = true;
			base.OnPointerEnter(eventData);
		}

		public override void OnPointerExit(PointerEventData eventData)
		{
			m_mouseOver = false;
			base.OnPointerExit(eventData);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (!m_mouseOver) return;
			Color.ApplyPressed(Image);
			m_pressed = true;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (!m_pressed) return;
			if (m_mouseOver || Selected) Color.ApplySelected(Image);
			else Color.ApplyEnabled(Image);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!m_mouseOver) return;
			var clicks = eventData.clickCount;
			if (clicks == 0) return;
			OnClick(clicks);
		}

		protected abstract void OnClick(int clicks);
	}
}