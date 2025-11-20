using Interactables;
using Managers;
using Managers.Persistent;
using Runtime;
using UI.Layout;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.Elements
{
	[AddComponentMenu("UI/Elements/Button")]
	public sealed class Button : ElementBase, IPointerEnterHandler, IPointerExitHandler, 
		IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, ILayoutCallback, ILayoutInputCallback, IPrewarm
	{
		[SerializeField] private Image image;
		[SerializeField] private ElementColor color;
		[SerializeField] private string primaryActionName = "Primary";
		[SerializeField] private string secondaryActionName = "Secondary";

		[SerializeField] private bool anyClick;
		[SerializeField] private Interactable onPrimaryClick;
		[SerializeField] [HideInInspector] private Interactable onSecondaryClick;

		[SerializeField] private bool debug;
		
		private InputAction m_primaryAction;
		private InputAction m_secondaryAction;
		private bool m_mouseOver;
		private bool m_selected;

		public void OnSelected()
		{
			color.ApplySelected(image);
			m_selected = true;
		}

		public void OnDeselected()
		{
			color.ApplyEnabled(image);
			m_selected = false;
		}
		
		public void OnInput(Vector2 axis, ref Direction input)
		{
			m_primaryAction ??= InputManager.Instance.GetUIAction(primaryActionName);
			m_secondaryAction ??= InputManager.Instance.GetUIAction(secondaryActionName);
			Interactable target = null;
			if (m_primaryAction.WasPressedThisFrame()) target = onPrimaryClick;
			else if (m_secondaryAction.WasPressedThisFrame()) target = onSecondaryClick;
			if (target) target.Interact();
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
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (!m_mouseOver) return;
			color.ApplySelected(image);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!m_mouseOver) return;
			var clicks = eventData.clickCount;
			if (clicks == 0) return;
			var interactable = onPrimaryClick;
			if (!anyClick && clicks != 1) interactable = onSecondaryClick;
			if (interactable) interactable.Interact();
		}
		
		public void OnPrewarm()
		{
			var layoutParent = LayoutParent;
			if (layoutParent)
			{
				layoutParent.SetCallback(this);
				layoutParent.SetControlCallback(this);
			}
		}

		private void OnEnable()
		{
			if (m_selected) color.ApplySelected(image);
			else color.ApplyEnabled(image);
		}

		private void OnDisable() => color.ApplyDisabled(image);

		private void OnValidate() => color.Validate(image, enabled);
	}
}