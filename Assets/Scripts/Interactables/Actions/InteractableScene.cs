using Managers;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Load New Scene")]
	public sealed class InteractableScene : Interactable
	{
		[SerializeField] private string sceneName;
		
		public override void Interact() => SceneManager.Instance.LoadNewScene(sceneName);
	}
}