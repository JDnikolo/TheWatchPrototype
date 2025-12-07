using Attributes;
using Callbacks.Dragging;
using Callbacks.Layout;
using Callbacks.Pointer;
using UI.Knob;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Elements
{
	[AddComponentMenu("UI/Elements/Sub/Knob")]
	public sealed class Knob : BaseBehaviour, IPointerEnterCallback, IPointerExitCallback,
		IBeginDragCallback, IDragCallback, IEndDragCallback, ILayoutCallback
	{
		[SerializeField] private Image image;
		[SerializeField] private ElementColor color;

		[SerializeField] [AutoAssigned(AssignMode.Self | AssignMode.Forced, typeof(DragReceptor))] 
		private DragReceptor dragReceptor;
		
		[SerializeField] [AutoAssigned(AssignMode.Self | AssignMode.Forced, typeof(PointerReceptor))] 
		private PointerReceptor pointerReceptor;

		private IKnobReceiver m_receiver;
		private bool m_mouseOver;
		private bool m_dragging;

		public RectTransform RectTransform => image.rectTransform;

		public void SetReceptors(bool enabled)
		{
			var drag = dragReceptor;
			if (drag) drag.enabled = enabled;
			var pointer = pointerReceptor;
			if (pointer) pointer.enabled = enabled;
		}
		
		public void SetReceiver(IKnobReceiver receiver) => m_receiver = receiver;

		public void OnSelected() => color.ApplySelected(image);

		public void OnDeselected() => color.ApplyEnabled(image);

		public void OnPointerEnter(PointerEventData eventData)
		{
			m_mouseOver = true;
			if (m_dragging) return;
			m_receiver?.OnKnobHover(true);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			m_mouseOver = false;
			if (m_dragging) return;
			m_receiver?.OnKnobHover(false);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (!m_mouseOver) return;
			m_dragging = true;
			color.ApplyPressed(image);
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (m_dragging) m_receiver?.OnKnobMovement(eventData.position);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (m_mouseOver) color.ApplySelected(image);
			else m_receiver?.OnKnobHover(false);
			
			m_dragging = false;
		}

		private void Awake()
		{
			var drag = dragReceptor;
			if (drag) drag.AddReceiver(this);
			var pointer = pointerReceptor;
			if (pointer) pointer.AddReceiver(this);
		}

		private void OnEnable() => color.ApplyEnabled(image);

		private void OnDisable() => color.ApplyDisabled(image);

		private void OnDestroy()
		{
			m_receiver = null;
			var drag = dragReceptor;
			if (drag) drag.RemoveReceiver(this);
			var pointer = pointerReceptor;
			if (pointer) pointer.RemoveReceiver(this);
		}
	}
}