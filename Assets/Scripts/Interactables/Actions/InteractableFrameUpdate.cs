using Managers;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Frame Update Interactable")]
	public sealed class InteractableFrameUpdate : Interactable
	{
		[SerializeField] private bool enable;
		
		public override void Interact()
		{
			var gameManager = GameManager.Instance;
			if (gameManager) gameManager.RequiresFrameUpdate = enable;
		}
	}
}