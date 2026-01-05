using Input;

namespace Callbacks.Backing
{
	public interface IBackHook
	{
		void OnBackPressed(InputState inputState);
	}
}