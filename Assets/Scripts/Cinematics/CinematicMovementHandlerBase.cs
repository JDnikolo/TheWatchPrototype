using Interactables;

namespace Cinematics
{
	internal abstract class CinematicMovementHandlerBase
	{
		public float MovementDuration { get; protected set; }
		
		public float MovementTimer { get; protected set; }
		
		public float Progress => MovementTimer / MovementDuration;

		public Interactable OnDurationEnd { get; protected set; }

		public bool IncreaseTimer(float deltaTime)
		{
			MovementTimer += deltaTime;
			if (MovementTimer >= MovementDuration) OnDurationEnd?.Interact();
			return MovementTimer > MovementDuration;
		}

		public abstract void ResolveMovement();
	}
}