using Callbacks.ComboBox;

namespace UI.ComboBox
{
	public struct ComboPanelInput
	{
		public Elements.ComboBox Parent;
		public IComboBoxFinished OnComboBoxFinished;

		public ComboPanelInput(Elements.ComboBox parent, IComboBoxFinished onComboBoxFinished)
		{
			Parent = parent;
			OnComboBoxFinished = onComboBoxFinished;
		}
	}
}