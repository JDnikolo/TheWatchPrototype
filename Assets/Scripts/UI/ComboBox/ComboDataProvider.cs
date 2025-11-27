using System.Collections.Generic;
using Runtime;
using UnityEngine;

namespace UI.ComboBox
{
	public abstract class ComboDataProvider : MonoBehaviour, IComboDataProvider, IPrewarm
	{
		private ComboData[] m_dataPoints;

		public IReadOnlyList<ComboData> DataPoints => m_dataPoints;

		public virtual void OnPrewarm() => CreateDataPoints(ref m_dataPoints);

		protected abstract void CreateDataPoints(ref ComboData[] dataPoints);
	}
}