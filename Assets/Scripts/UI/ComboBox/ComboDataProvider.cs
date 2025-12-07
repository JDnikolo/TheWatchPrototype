using System.Collections.Generic;
using Callbacks.Prewarm;

namespace UI.ComboBox
{
	public abstract class ComboDataProvider : BaseBehaviour, IComboDataProvider, IPrewarm
	{
		private ComboData[] m_dataPoints;

		public IReadOnlyList<ComboData> DataPoints => m_dataPoints;
		
		public abstract ComboData CurrentData { get; }

		public virtual void OnPrewarm() => CreateDataPoints(ref m_dataPoints);

		protected abstract void CreateDataPoints(ref ComboData[] dataPoints);
	}
}