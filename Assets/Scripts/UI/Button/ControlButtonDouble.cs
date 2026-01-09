using Attributes;
using Callbacks.Layout;
using Callbacks.Prewarm;
using Input;
using Localization.Text;
using Managers.Persistent;
using UI.Elements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.Button
{
	[AddComponentMenu("UI/Button/Assign Control Button with Secondary")]
	public sealed class ControlButtonDouble : BaseBehaviour, ILayoutCallback, IPrewarm
	{
		[SerializeField] private Image image;
		[SerializeField] private ElementColor color;
		[SerializeField] private ControlButton firstButton;
		[SerializeField] private ControlButton secondButton;
		
		private bool m_selected;

		public void OnSelected()
		{
			m_selected = true;
			color.ApplySelected(image);
		}

		public void OnDeselected()
		{
			m_selected = false;
			color.ApplyEnabled(image);
		}

		public void OnPrewarm()
		{
			//if (LayoutParent) LayoutParent.SetCallback(this);
		}

		private void OnEnable()
		{
			if (m_selected) color.ApplySelected(image);
			else color.ApplyEnabled(image);
		}

		private void OnDisable() => color.ApplyDisabled(image);
#if UNITY_EDITOR
		//Input info
		[CanBeNullInPrefab, SerializeField] private InputActionReference target;
		[SerializeField] private ControlSchemeEnum scheme;
		[SerializeField] private string group;
		
		//Control info
		[CanBeNullInPrefab, SerializeField] private TextObject text;
		[SerializeField] private Label label;

		private void OnValidate()
		{
			if (image && color) color.Validate(image, enabled);
			if (label && text) label.ManagedTextToDisplay = text;
			if (!target) return;
			var action = target.action;
			var bindings = InputManager.GetBindingIndexes(action, scheme);
			if (bindings.Secondary < 0)
			{
				Debug.LogError($"{target} has no secondary value, do not use double button for this!");
				return;
			}

			if (firstButton) firstButton.SetFromParent(this, target, scheme, group, false);
			if (secondButton) secondButton.SetFromParent(this, target, scheme, group, true);
		}
#endif
	}
}