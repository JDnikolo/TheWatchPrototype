using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions.Scene
{
	[AddComponentMenu("Interactables/Scene/Prepare Scene Loading")]
	public sealed class InteractablePrepareSceneLoad : Interactable
	{
		public override void Interact()
		{
			AudioManager.Instance.StopMusic();
			InputManager.Instance.Stop();
			PhysicsManager.Instance.Stop();
		}
	}
}