using Night;

namespace Callbacks.Night
{
	public interface ITimeChangedCallback
	{
		void OnTimeChanged(NightTime time);
	}
}