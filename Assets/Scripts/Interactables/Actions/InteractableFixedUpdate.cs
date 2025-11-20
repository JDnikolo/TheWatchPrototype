using Managers.Persistent;
using Runtime.FixedUpdate;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Fixed Update Interactable")]
	public sealed class InteractableFixedUpdate : Interactable
	{
		[SerializeField] private bool enable;
		
		public override void Interact()
		{
			if (enable) GameManager.Instance.FixedUpdateInvoke = FixedUpdatePosition.All;
			else GameManager.Instance.FixedUpdateInvoke = FixedUpdatePosition.None;
		}
	}
}