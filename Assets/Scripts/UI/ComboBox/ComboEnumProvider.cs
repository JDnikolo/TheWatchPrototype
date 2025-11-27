using System;
using Localization.Text;
using UnityEngine;
using Utilities;

namespace UI.ComboBox
{
	public abstract class ComboEnumProvider<T> : ComboDataProvider where T : struct, Enum
	{
		[SerializeField] private EnumText localization;
		
		protected override void CreateDataPoints(ref ComboData[] dataPoints)
		{
			var values = Enum.GetValues(typeof(T));
			var length = values.Length;
			if (values.GetValue(length - 1).ToString() == Utils.ENUM_LENGTH) length -= 1;
			var array = new ComboData[length];
			for (var i = 0; i < length; i++)
			{
				var value = (T) values.GetValue(i);
				array[i] = new ComboData(localization.Values[i], value.MakeImmutableRef());
			}

			dataPoints = array;
		}
	}
}