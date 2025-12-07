using UnityEngine;

namespace Interactables.Triggers
{
	[AddComponentMenu("Interactables/Triggers/On Scene-Start Trigger")]
	public sealed class InteractableSceneStartTrigger : InteractableTrigger
	{
		private void Start() => OnInteract();
	}
}