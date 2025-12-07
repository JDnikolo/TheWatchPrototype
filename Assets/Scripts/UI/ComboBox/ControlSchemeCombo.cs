using Attributes;
using Boxing;
using Callbacks.ComboBox;
using Input;
using Managers.Persistent;
using UnityEngine;

namespace UI.ComboBox
{
	[AddComponentMenu("UI/Elements/ComboBox/Control Scheme Combo")]
	public sealed class ControlSchemeCombo : ComboEnumProvider<ControlSchemeEnum>, IComboBoxReceiver
	{
		[CanBeNullInPrefab, SerializeField] private Elements.ComboBox comboBox;

		public override ComboData CurrentData => DataPoints[(int) InputManager.Instance.ControlScheme];
		
		public void OnComboBoxSelectionChanged(ComboData data)
		{
			if (data.UserData is IRef<ControlSchemeEnum> reference) 
				InputManager.Instance.ControlScheme = reference.GetValue();
		}

		public override void OnPrewarm()
		{
			base.OnPrewarm();
			comboBox.SetReceiver(this);
		}
	}
}