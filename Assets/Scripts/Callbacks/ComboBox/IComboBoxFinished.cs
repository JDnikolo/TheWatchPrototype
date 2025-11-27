using UI.ComboBox;

namespace Callbacks.ComboBox
{
	public interface IComboBoxFinished
	{
		void OnComboBoxFinished();
		
		void OnComboBoxFinished(ComboData data);
	}
}