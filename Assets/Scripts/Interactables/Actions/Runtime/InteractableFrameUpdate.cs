using Managers.Persistent;
using Runtime.FrameUpdate;
using UnityEngine;

namespace Interactables.Actions.Runtime
{
	[AddComponentMenu("Interactables/Runtime/Toggle Frame Update")]
	public sealed class InteractableFrameUpdate : Interactable
	{
		[SerializeField] private bool enable;
		
		public override void Interact()
		{
			if (enable) GameManager.Instance.FrameUpdateInvoke = FrameUpdatePosition.All;
			else GameManager.Instance.FrameUpdateInvoke = FrameUpdatePosition.None;
		}
	}
}