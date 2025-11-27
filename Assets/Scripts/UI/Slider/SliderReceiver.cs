using Callbacks.Slider;
using Runtime;
using UnityEngine;

namespace UI.Slider
{
	public abstract class SliderReceiver : MonoBehaviour, ISliderReceiver, IPrewarm
	{
		[SerializeField] private Elements.Slider slider;
		
		protected Elements.Slider Slider => slider;
		
		public abstract void OnSliderChanged(float value);

		public abstract void OnSliderChanged(int value);

		public virtual void OnPrewarm() => slider.SetReceiver(this);
	}
}