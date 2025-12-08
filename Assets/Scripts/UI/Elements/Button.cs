using System;
using Attributes;
using Callbacks.Layout;
using Debugging;
using Interactables;
using Managers.Persistent;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace UI.Elements
{
	[AddComponentMenu("UI/Elements/Button")]
	public sealed class Button : ButtonBase, ILayoutInputCallback
	{
		[SerializeField] private string primaryActionName = "Primary";
		[SerializeField] private string secondaryActionName = "Secondary";

		[SerializeField, HideInInspector] private bool anyClick;
		
		[CanBeNullInPrefab, SerializeField, HideInInspector]
		private Interactable onPrimaryClick;
#if UNITY_EDITOR
		[CustomDebug(nameof(DebugSecondary))]
#endif
		[SerializeField, HideInInspector]
		private Interactable onSecondaryClick;

		private InputAction m_primaryAction;
		private InputAction m_secondaryAction;

		private void OnPrimaryClick() => onPrimaryClick.Interact();

		private void OnSecondaryClick() => onSecondaryClick.Interact();

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
#if UNITY_EDITOR
		private bool DebugSecondary(OperationData operationData, string path, FieldData fieldData) => 
			anyClick || path.IsAssetPath() || operationData.TestObject(path, fieldData, onSecondaryClick);
#endif
	}
}