using UnityEngine;

namespace UI.Layout
{
	public interface ILayoutInputCallback
	{
		void OnInput(Vector2 axis, ref Direction input);
	}
}