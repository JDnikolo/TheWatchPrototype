using UI.ComboBox;

namespace Callbacks.ComboBox
{
	public interface IComboBoxReceiver
	{
		void OnComboBoxSelectionChanged(ComboData data);
	}
}