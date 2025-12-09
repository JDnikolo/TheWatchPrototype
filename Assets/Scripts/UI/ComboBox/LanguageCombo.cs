using Attributes;
using Boxing;
using Callbacks.ComboBox;
using Localization;
using Managers.Persistent;
using UnityEngine;

namespace UI.ComboBox
{
	[AddComponentMenu("UI/Elements/ComboBox/Language ComboBox")]
	public sealed class LanguageCombo : ComboEnumProvider<LanguageEnum>, IComboBoxReceiver
	{
		[SerializeField] [AutoAssigned(AssignModeFlags.Child, typeof(Elements.ComboBox))]
		private Elements.ComboBox comboBox;

		public override ComboData CurrentData => DataPoints[(int) LanguageManager.Instance.Language];
		
		public void OnComboBoxSelectionChanged(ComboData data)
		{
			if (data.UserData is not IRef<LanguageEnum> reference) return;
			LanguageManager.Instance.Language = reference.GetValue();
			SettingsManager.Instance.Save();
		}

		public override void OnPrewarm()
		{
			base.OnPrewarm();
			comboBox.SetReceiver(this);
		}
	}
}