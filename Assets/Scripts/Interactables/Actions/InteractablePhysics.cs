using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Physics Interactable")]
	public sealed class InteractablePhysics : Interactable
	{
		[SerializeField] private bool enable;
		
		public override void Interact() => PhysicsManager.Instance.RequirePhysics = enable;
	}
}