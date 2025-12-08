using Managers.Persistent;
using UnityEngine;
using Utilities;

namespace Interactables.Actions.Input
{
	[AddComponentMenu("Interactables/Input/Enable Player Input")]
	public sealed class InteractablePlayerInput : Interactable
	{
		[SerializeField] private bool disable;
		
		public override void Interact()
		{
			if (disable) InputManager.Instance.PlayerMap.Enabled = false;
			InputManager.Instance.ForcePlayerInput();
		}
	}
}