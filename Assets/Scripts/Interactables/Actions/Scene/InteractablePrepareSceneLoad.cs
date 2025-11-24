using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions.Scene
{
	[AddComponentMenu("Interactables/Scene/Prepare Scene Loading")]
	public sealed class InteractablePrepareSceneLoad : Interactable
	{
		public override void Interact()
		{
			PhysicsManager.Instance.Stop();
			InputManager.Instance.Stop();
		}
	}
}