using Managers.Persistent;
using Runtime.LateUpdate;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Late Update Interactable")]
	public sealed class InteractableLateUpdate : Interactable
	{
		[SerializeField] private bool enable;
		
		public override void Interact()
		{
			if (enable) GameManager.Instance.LateUpdateInvoke = LateUpdatePosition.All;
			else GameManager.Instance.LateUpdateInvoke = LateUpdatePosition.None;
		}
	}
}