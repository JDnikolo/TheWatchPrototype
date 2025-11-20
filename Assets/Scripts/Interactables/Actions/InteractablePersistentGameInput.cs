using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Enable Persistent Game Input")]
	public sealed class InteractablePersistentGameInput : Interactable
	{
		public override void Interact() => InputManager.Instance.RequiresPersistentGameMap = true;
	}
}