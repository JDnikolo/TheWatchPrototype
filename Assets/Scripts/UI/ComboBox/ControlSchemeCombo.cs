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
		[SerializeField] private Elements.ComboBox comboBox;

		public void OnComboBoxSelectionChanged(ComboData data)
		{
			if (data.UserData is IRef<ControlSchemeEnum> reference) 
				InputManager.Instance.SetNewControlScheme(reference.GetValue());
		}

		public override void OnPrewarm()
		{
			base.OnPrewarm();
			comboBox.SetReceiver(this);
			comboBox.SetData(DataPoints[(int) InputManager.Instance.ControlScheme], false);
		}
	}
}