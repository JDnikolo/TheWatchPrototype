using UnityEngine;

namespace UI.Elements
{
	public interface IKnobReceiver
	{
		void OnKnobHover(bool hover);

		void OnKnobMovement(Vector2 delta);
	}
}