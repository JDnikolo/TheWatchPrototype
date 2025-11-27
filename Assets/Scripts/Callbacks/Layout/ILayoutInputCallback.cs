using UI;
using UnityEngine;

namespace Callbacks.Layout
{
	public interface ILayoutInputCallback
	{
		void OnInput(Vector2 axis, ref Direction input);
	}
}