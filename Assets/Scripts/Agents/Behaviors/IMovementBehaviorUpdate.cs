using UnityEngine;

namespace Agents.Behaviors
{
	public interface IMovementBehaviorUpdate
	{
		void UpdateMovement(Vector3 position);
	}
}