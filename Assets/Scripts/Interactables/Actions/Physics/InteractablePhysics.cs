using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions.Physics
{
	[AddComponentMenu("Interactables/Physics/Toggle Physics")]
	public sealed class InteractablePhysics : Interactable
	{
		[SerializeField] private bool enable;
		
		public override void Interact() => PhysicsManager.Instance.RequirePhysics = enable;
	}
}