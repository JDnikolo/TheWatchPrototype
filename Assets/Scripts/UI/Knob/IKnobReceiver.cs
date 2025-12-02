using UnityEngine;

namespace UI.Knob
{
	public interface IKnobReceiver
	{
		void OnKnobHover(bool hover);

		void OnKnobMovement(Vector2 screenPosition);
	}
}