namespace Callbacks.Slider
{
	public interface ISliderReceiver
	{
		void OnSliderChanged(float value);

		void OnSliderChanged(int value);
	}
}