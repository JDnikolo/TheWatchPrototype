using System.Collections.Generic;

namespace UI.ComboBox
{
	public interface IComboDataProvider
	{
		IReadOnlyList<ComboData> DataPoints { get; }
	}
}