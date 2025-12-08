using Attributes;
using Callbacks.Beforeplay;
using Callbacks.Layout;
using Debugging;
using Input;
using Localization.Text;
using Managers;
using Managers.Persistent;
using UI.Elements;
using UI.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace UI.Button
{
	[AddComponentMenu("UI/Button/Assign Control Button")]
	public sealed class ControlButton : ButtonBase, ILayoutInputCallback, IBeforePlay
	{
		[SerializeField] private string primaryActionName = "Primary";

		[SerializeField] [AutoAssigned(AssignModeFlags.Self, typeof(TextWriter))]
		private TextWriter textWriter;

		[SerializeField] [HideInInspector] private FullControlEnum target;
		[SerializeField] [HideInInspector] private bool secondary;

		private InputActionRebindingExtensions.RebindingOperation m_rebindingOperation;
		private InputAction m_action;
		private int m_bindingIndex;

		private InputAction m_primaryAction;
		private bool m_deselectFirst;
		private bool m_enable;

		public void OnInput(Vector2 axis, ref Direction input)
		{
			if (m_primaryAction.WasReleasedThisFrame())
			{
				if (m_deselectFirst) m_deselectFirst = false;
				else OnClick(-1);
			}
		}

		protected override void Select()
		{
			base.Select();
			if (m_primaryAction.IsPressed()) m_deselectFirst = true;
		}

		protected override void OnClick(int clicks)
		{
			if (LayoutParent) LayoutManager.Instance.Select(UIManager.Instance.ControlPanel);
			m_enable = m_action.actionMap.enabled;
			m_action.actionMap.Disable();
			m_rebindingOperation = m_action.PerformInteractiveRebinding(m_bindingIndex)
				.OnComplete(OnCompleted).OnCancel(OnCancel);
			m_rebindingOperation.Start();
		}

		private void OnCompleted(InputActionRebindingExtensions.RebindingOperation obj)
		{
			m_rebindingOperation.Dispose();
			SetDisplayString();
			if (m_enable) m_action.actionMap.Enable();
			SettingsManager.Instance.SetString(nameof(InputManager.Instance.BindingOverridesJson),
				InputManager.Instance.BindingOverridesJson);
			ResetToThis();
		}

		private void OnCancel(InputActionRebindingExtensions.RebindingOperation obj)
		{
			m_rebindingOperation.Dispose();
			if (m_enable) m_action.actionMap.Enable();
			ResetToThis();
		}

		private void ResetToThis()
		{
			if (LayoutParent) LayoutManager.Instance.Select(LayoutParent);
		}

		private void SetDisplayString() =>
			textWriter.WriteText(m_action.ToBindingDisplayString(m_bindingIndex));

		public override void OnPrewarm()
		{
			base.OnPrewarm();
			if (LayoutParent) LayoutParent.SetInputCallback(this);
		}

		public void OnBeforePlay()
		{
			var inputManager = InputManager.Instance;
			m_action = inputManager.GetAction(InputManager.GetGroupedControl(target));
			m_primaryAction = inputManager.UIMap.GetAction(primaryActionName);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			m_bindingIndex = InputManager.Instance.GetBindingIndex(target, ref m_action, secondary);
			SetDisplayString();
		}
#if UNITY_EDITOR
		// ReSharper disable once MissingLinebreak
		[CanBeNull, SerializeField, HideInInspector]
		private ControlButtonDouble parent;

		// ReSharper disable once MissingLinebreak
		[CustomDebug(nameof(DebugLabel)), SerializeField, HideInInspector]
		private Label label;

		// ReSharper disable once MissingLinebreak
		[CustomDebug(nameof(DebugText)), SerializeField, HideInInspector]
		private InputActionText text;

		public void SetFromParent(ControlButtonDouble newParent, FullControlEnum newTarget, bool newSecondary)
		{
			this.DirtyReplaceObject(ref parent, newParent);
			this.DirtyReplaceValue(ref target, newTarget);
			this.DirtyReplaceValue(ref secondary, newSecondary);
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			if (parent && parent.transform != transform.parent) SetFromParent(null, default, false);
			if (label && text && text.Values.TryGetValue((int) target, out var value))
				label.ManagedTextToDisplay = value;
			if (secondary && !InputManager.HasSecondary(target))
				this.DirtyReplaceValue(ref secondary, false);
		}

		private bool DebugLabel(OperationData operationData, string path, FieldData fieldData) =>
			parent != null || path.IsAssetPath() || operationData.TestObject(path, fieldData, label);

		private bool DebugText(OperationData operationData, string path, FieldData fieldData) =>
			parent != null || path.IsAssetPath() || operationData.TestObject(path, fieldData, text);
#endif
	}
}