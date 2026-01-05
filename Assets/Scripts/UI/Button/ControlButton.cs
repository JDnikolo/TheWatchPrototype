using Attributes;
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
	public sealed class ControlButton : ButtonBase, ILayoutInputCallback
	{
		[SerializeField] private InputActionReference inputReference;
		[SerializeField] [AutoAssigned(AssignModeFlags.Self, typeof(TextWriter))]
		private TextWriter textWriter;

		[SerializeField, HideInInspector] private InputActionReference target;
		[SerializeField, HideInInspector] private ControlSchemeEnum scheme;
		[SerializeField, HideInInspector] private string group;
		[SerializeField, HideInInspector] private bool hasSecondary;
		[SerializeField, HideInInspector] private bool secondary;

		private InputActionRebindingExtensions.RebindingOperation m_rebindingOperation;
		private int m_bindingIndex;
		private bool m_deselectFirst;
		private bool m_enable;

		public void OnInput(Vector2 axis, ref Direction input)
		{
			if (inputReference.action.WasReleasedThisFrame())
			{
				if (m_deselectFirst) m_deselectFirst = false;
				else OnClick(-1);
			}
		}

		protected override void Select()
		{
			base.Select();
			if (inputReference.action.IsPressed()) m_deselectFirst = true;
		}

		protected override void OnClick(int clicks)
		{
			if (LayoutParent) LayoutManager.Instance.Select(UIManager.Instance.ControlPanel);
			var action = target.action;
			m_enable = action.actionMap.enabled;
			action.actionMap.Disable();
			m_rebindingOperation = action.PerformInteractiveRebinding(m_bindingIndex)
				.OnComplete(OnCompleted).OnCancel(OnCancel);
			m_rebindingOperation.Start();
		}

		private void OnCompleted(InputActionRebindingExtensions.RebindingOperation obj)
		{
			m_rebindingOperation.Dispose();
			SetDisplayString();
			if (m_enable) target.action.actionMap.Enable();
			var inputManager = InputManager.Instance;
			var settingsManager = SettingsManager.Instance;
			settingsManager.SetString(nameof(inputManager.BindingOverridesJson), inputManager.BindingOverridesJson);
			settingsManager.Save();
			ResetToThis();
		}

		private void OnCancel(InputActionRebindingExtensions.RebindingOperation obj)
		{
			m_rebindingOperation.Dispose();
			if (m_enable) target.action.actionMap.Enable();
			ResetToThis();
		}

		private void ResetToThis()
		{
			if (LayoutParent) LayoutManager.Instance.Select(LayoutParent);
		}

		private void SetDisplayString() =>
			textWriter.WriteText(target.action.ToBindingDisplayString(m_bindingIndex));

		public override void OnPrewarm()
		{
			base.OnPrewarm();
			if (LayoutParent) LayoutParent.SetInputCallback(this);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			m_bindingIndex = InputManager.Instance.GetBindingIndex(target.action, secondary,
				string.IsNullOrWhiteSpace(group) ? null : group);
			SetDisplayString();
		}
#if UNITY_EDITOR
		// ReSharper disable once MissingLinebreak
		[CanBeNull, SerializeField, HideInInspector]
		private ControlButtonDouble parent;

		// ReSharper disable once MissingLinebreak
		[CustomDebug(nameof(DebugText)), SerializeField, HideInInspector]
		private TextObject text;
		
		// ReSharper disable once MissingLinebreak
		[CustomDebug(nameof(DebugLabel)), SerializeField, HideInInspector]
		private Label label;

		public void SetFromParent(ControlButtonDouble newParent, InputActionReference newTarget, 
			ControlSchemeEnum newScheme, string newGroup, bool newSecondary)
		{
			this.DirtyReplaceObject(ref parent, newParent);
			this.DirtyReplaceObject(ref target, newTarget);
			this.DirtyReplaceValue(ref scheme, newScheme);
			this.DirtyReplaceReference(ref group, newGroup);
			this.DirtyReplaceValue(ref secondary, newSecondary);
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			if (parent && parent.transform != transform.parent) SetFromParent(null, null, default, null, false);
			if (label && text) label.ManagedTextToDisplay = text;
			if (secondary && InputManager.GetBindingIndex(target, secondary, scheme) < 0)
				this.DirtyReplaceValue(ref secondary, false);
		}

		private bool DebugLabel(OperationData operationData, string path, FieldData fieldData) =>
			DebugPath(path) || operationData.TestObject(path, fieldData, label);

		private bool DebugText(OperationData operationData, string path, FieldData fieldData) =>
			DebugPath(path) || operationData.TestObject(path, fieldData, text);

		private bool DebugPath(string path) => parent != null || path.IsAssetPath();
#endif
	}
}