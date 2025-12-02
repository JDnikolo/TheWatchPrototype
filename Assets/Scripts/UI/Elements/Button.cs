using System;
using Callbacks.Layout;
using Interactables;
using Managers.Persistent;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Elements
{
	[AddComponentMenu("UI/Elements/Button")]
	public sealed class Button : ButtonBase, ILayoutInputCallback
	{
		[SerializeField] private string primaryActionName = "Primary";
		[SerializeField] private string secondaryActionName = "Secondary";
		
		[SerializeField] [HideInInspector] private bool anyClick;
		[SerializeField] [HideInInspector] private Interactable onPrimaryClick;
		[SerializeField] [HideInInspector] private Interactable onSecondaryClick;
		
		private InputAction m_primaryAction;
		private InputAction m_secondaryAction;
		
		private void OnPrimaryClick()
		{
			if (onPrimaryClick) onPrimaryClick.Interact();
		}

		private void OnSecondaryClick()
		{
			if (onSecondaryClick) onSecondaryClick.Interact();
		}
		
		public void OnInput(Vector2 axis, ref Direction input)
		{
			m_primaryAction ??= InputManager.Instance.UIMap.GetAction(primaryActionName);
			m_secondaryAction ??= InputManager.Instance.UIMap.GetAction(secondaryActionName);
			Action target = null;
			if (m_primaryAction.WasPressedThisFrame()) target = OnPrimaryClick;
			else if (m_secondaryAction.WasPressedThisFrame()) target = anyClick ? OnPrimaryClick : OnSecondaryClick;
			target?.Invoke();
		}

		protected override void OnClick(int clicks)
		{
			Action target = OnPrimaryClick;
			if (!anyClick && clicks != 1) target = OnSecondaryClick;
			target.Invoke();
		}

		public override void OnPrewarm()
		{
			base.OnPrewarm();
			var layoutParent = LayoutParent;
			if (layoutParent) layoutParent.SetInputCallback(this);
		}
	}
}