using Callbacks.Layout;
using Callbacks.Prewarm;
using Input;
using Localization.Text;
using Managers.Persistent;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI.Button
{
	[AddComponentMenu("UI/Button/Assign Control Button with Secondary")]
	public sealed class ControlButtonDouble : ElementBase, ILayoutCallback, IPrewarm
	{
		[SerializeField] private Image image;
		[SerializeField] private ElementColor color;

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
			if (LayoutParent) LayoutParent.SetCallback(this);
		}
		
		private void OnEnable()
		{
			if (m_selected) color.ApplySelected(image);
			else color.ApplyEnabled(image);
		}

		private void OnDisable() => color.ApplyDisabled(image);
#if UNITY_EDITOR
		[SerializeField] private ControlEnum target;
		[SerializeField] private Label label;
		[SerializeField] private InputActionText text;
		[SerializeField] private ControlButton firstButton;
		[SerializeField] private ControlButton secondButton;
		
		private void OnValidate()
		{
			if (image && color) color.Validate(image, enabled);
			if (label && text && text.Values.TryGetValue((int) target, out var value))
			{
				label.SetManagedTextToDisplay(value);
				this.UpdateNameTo(value);
			}

			if (!InputManager.HasSecondary(target))
			{
				Debug.LogError($"{target} has no secondary value, do not use double button for this!");
				return;
			}
			
			if (firstButton) firstButton.SetFromParent(target, false);
			if (secondButton) secondButton.SetFromParent(target, true);
		}
#endif
	}
}