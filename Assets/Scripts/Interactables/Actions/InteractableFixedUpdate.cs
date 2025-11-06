using Managers;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Fixed Update Interactable")]
	public sealed class InteractableFixedUpdate : Interactable
	{
		[SerializeField] private bool enable;
		
		public override void Interact()
		{
			var gameManager = GameManager.Instance;
			if (gameManager) gameManager.RequiredFixedUpdate = enable;
		}
	}
}