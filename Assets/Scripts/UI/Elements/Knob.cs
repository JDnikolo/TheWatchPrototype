using Callbacks.Layout;
using UI.Knob;
using UI.Layout;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Elements
{
	[AddComponentMenu("UI/Elements/Sub/Knob")]
	public sealed class Knob : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
		IBeginDragHandler, IDragHandler, IEndDragHandler, ILayoutCallback
	{
		[SerializeField] private Image image;
		[SerializeField] private ElementColor color;

		private IKnobReceiver m_receiver;
		private bool m_mouseOver;
		private bool m_dragging;

		public RectTransform RectTransform => image.rectTransform;

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
			if (m_dragging) m_receiver?.OnKnobMovement(eventData.delta);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (m_mouseOver) color.ApplySelected(image);
			else
			{
				m_receiver?.OnKnobHover(false);
				color.ApplyEnabled(image);
			}

			m_dragging = false;
		}

		private void OnEnable() => color.ApplyEnabled(image);

		private void OnDisable() => color.ApplyDisabled(image);

		private void OnDestroy() => m_receiver = null;
	}
}