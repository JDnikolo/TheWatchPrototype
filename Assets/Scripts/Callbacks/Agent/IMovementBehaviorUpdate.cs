using UnityEngine;

namespace Callbacks.Agent
{
	public interface IMovementBehaviorUpdate
	{
		void UpdateMovement(Vector3 position);
	}
}