using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Physics 2D Interactable")]
	public sealed class InteractablePhysics2D : Interactable
	{
		[SerializeField] private bool enable;
		
		public override void Interact() => PhysicsManager.Instance.RequirePhysics2D = enable;
	}
}