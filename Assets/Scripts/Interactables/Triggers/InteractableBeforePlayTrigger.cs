using Callbacks.Beforeplay;
using UnityEngine;

namespace Interactables.Triggers
{
	[AddComponentMenu("Interactables/Triggers/On Before-Play Trigger")]
	public sealed class InteractableBeforePlayTrigger : InteractableTrigger, IBeforePlay
	{
		public void OnBeforePlay() => OnInteract();
	}
}