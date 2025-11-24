using Managers;
using UnityEngine;

namespace Interactables.Actions.Pause
{
	[AddComponentMenu("Interactables/Pause/Set Can Pause")]
	public sealed class InteractablePause : Interactable
	{
		[SerializeField] private bool target;
		
		public override void Interact() => PauseManager.Instance.CanPause = target;
	}
}