using Attributes;
using Callbacks.Prewarm;
using Callbacks.Slider;
using UnityEngine;

namespace UI.Slider
{
	public abstract class SliderFloatReceiver : BaseBehaviour, ISliderFloatReceiver, IPrewarm
	{
		[SerializeField] [AutoAssigned(AssignMode.Child, typeof(Elements.Slider))]
		private Elements.Slider slider;
		
		protected Elements.Slider Slider => slider;
		
		public abstract void OnSliderChanged(float value);

		public virtual void OnPrewarm() => slider.SetFloatReceiver(this);
	}
}