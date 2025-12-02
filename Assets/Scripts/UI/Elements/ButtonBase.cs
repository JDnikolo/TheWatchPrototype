using Attributes;
using Callbacks.Layout;
using Callbacks.Pointer;
using Callbacks.Prewarm;
using Managers;
using Runtime.Automation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Elements
{
	public abstract class ButtonBase : ElementBase, IPointerEnterCallback, IPointerExitCallback,
		IPointerDownCallback, IPointerUpCallback, IPointerClickCallback, ILayoutCallback, IPrewarm
	{
		[SerializeField] [AutoAssigned(AssignMode.Self, typeof(RectTransform))]  
		private RectTransform rectTransform;

		[SerializeField] [AutoAssigned(AssignMode.Self, typeof(Image))]
		private Image image;
		
		[SerializeField] private ElementColor color;

		[SerializeField] [AutoAssigned(AssignMode.Self, typeof(PointerReceptor))] 
		private PointerReceptor pointerReceptor;
		
		protected RectTransform RectTransform => rectTransform;
		
		private bool m_mouseOver;
		private bool m_pressed;
		private bool m_selected;

		public virtual void OnSelected()
		{
			color.ApplySelected(image);
			m_selected = true;
		}

		public virtual void OnDeselected()
		{
			color.ApplyEnabled(image);
			m_selected = false;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			m_mouseOver = true;
			if (LayoutParent) LayoutManager.Instance.Select(LayoutParent);
			else OnSelected();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			m_mouseOver = false;
			if (!LayoutParent) OnDeselected();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (!m_mouseOver) return;
			color.ApplyPressed(image);
			m_pressed = true;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (!m_pressed) return;
			if (m_mouseOver || m_selected) color.ApplySelected(image);
			else color.ApplyEnabled(image);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!m_mouseOver) return;
			var clicks = eventData.clickCount;
			if (clicks == 0) return;
			OnClick(clicks);
		}

		protected abstract void OnClick(int clicks);

		public virtual void OnPrewarm()
		{
			var layoutParent = LayoutParent;
			if (layoutParent) layoutParent.SetCallback(this);
			var pointer = pointerReceptor;
			if (pointer) pointer.AddReceiver(this);
		}

		protected virtual void OnEnable()
		{
			if (m_selected) color.ApplySelected(image);
			else color.ApplyEnabled(image);
		}

		protected virtual void OnDisable() => color.ApplyDisabled(image);

		protected virtual void OnDestroy()
		{
			var pointer = pointerReceptor;
			if (pointer) pointer.RemoveReceiver(this);
		}
#if UNITY_EDITOR
		public bool Selected => m_selected;

		protected virtual void OnValidate()
		{
			if (image && color) color.Validate(image, enabled);
		}
#endif
	}
}