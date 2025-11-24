using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions.Input
{
	[AddComponentMenu("Interactables/Input/Enable Persistent Game Input")]
	public sealed class InteractablePersistentGameInput : Interactable
	{
		public override void Interact() => InputManager.Instance.PersistentGameMap.Enabled = true;
	}
}