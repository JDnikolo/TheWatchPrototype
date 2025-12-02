using Attributes;
using Boxing;
using Callbacks.ComboBox;
using Localization;
using Managers.Persistent;
using Runtime.Automation;
using UnityEngine;

namespace UI.ComboBox
{
	[AddComponentMenu("UI/Elements/ComboBox/Language ComboBox")]
	public sealed class LanguageCombo : ComboEnumProvider<LanguageEnum>, IComboBoxReceiver
	{
		[SerializeField] [AutoAssigned(AssignMode.Child, typeof(Elements.ComboBox))]
		private Elements.ComboBox comboBox;

		public override ComboData CurrentData => DataPoints[(int) LanguageManager.Instance.Language];
		
		public void OnComboBoxSelectionChanged(ComboData data)
		{
			if (data.UserData is IRef<LanguageEnum> reference) 
				LanguageManager.Instance.Language = reference.GetValue();
		}

		public override void OnPrewarm()
		{
			base.OnPrewarm();
			comboBox.SetReceiver(this);
		}
	}
}