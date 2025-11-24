using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions.Physics
{
	[AddComponentMenu("Interactables/Physics/Toggle Physics 2D")]
	public sealed class InteractablePhysics2D : Interactable
	{
		[SerializeField] private bool enable;
		
		public override void Interact() => PhysicsManager.Instance.RequirePhysics2D = enable;
	}
}