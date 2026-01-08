using System;
using Attributes;
using Callbacks.Layout;
using Debugging;
using Interactables;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace UI.Elements
{
	[AddComponentMenu("UI/Elements/Button")]
	public class Button : ButtonBase, ILayoutInputCallback
	{
		[SerializeField, HideInInspector] private InputActionReference primaryReference;
#if UNITY_EDITOR
		[CustomDebug(nameof(DebugSecondary))]
#endif
		[SerializeField, HideInInspector] 
		private InputActionReference secondaryReference;
		
		[SerializeField, HideInInspector] private bool anyClick;
		
		[CanBeNullInPrefab, SerializeField, HideInInspector]
		private Interactable onPrimaryClick;
#if UNITY_EDITOR
		[CustomDebug(nameof(DebugSecondary))]
#endif
		[SerializeField, HideInInspector]
		private Interactable onSecondaryClick;

		private void OnPrimaryClick() => onPrimaryClick.Interact();

		private void OnSecondaryClick() => onSecondaryClick.Interact();

		public void OnInput(Vector2 axis, ref Direction input)
		{
			Action target = null;
			if (anyClick)
			{
				if (primaryReference.action.WasPressedThisFrame()) target = OnPrimaryClick;
			}
			else if (primaryReference.action.WasPressedThisFrame()) target = OnPrimaryClick;
			else if (secondaryReference.action.WasPressedThisFrame())
				target = anyClick ? OnPrimaryClick : OnSecondaryClick;

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