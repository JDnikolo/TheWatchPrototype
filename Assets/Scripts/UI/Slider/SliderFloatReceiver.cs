using Attributes;
using Callbacks.Slider;
using Managers.Persistent;
using UnityEngine;

namespace UI.Slider
{
	public abstract class SliderFloatReceiver : BaseBehaviour, ISliderReceiver, ISliderFloatReceiver
	{
		[SerializeField] [AutoAssigned(AssignModeFlags.Child, typeof(Elements.Slider))]
		private Elements.Slider slider;
		
		protected Elements.Slider Slider => slider;
		
		public abstract void OnSliderChanged(float value);
		
		public void OnSliderFinished() => SettingsManager.Instance.Save();

		protected virtual void OnEnable()
		{
			slider.SetFloatReceiver(this);
			slider.SetReceiver(this);
		}
	}
}