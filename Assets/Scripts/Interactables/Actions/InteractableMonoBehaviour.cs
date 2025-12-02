using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Toggle MonoBehaviour")]
	public sealed class InteractableMonoBehaviour : Interactable
	{
		[SerializeField] private MonoBehaviour target;
		[SerializeField] private bool enable;
		
		public override void Interact()
		{
			if (target) target.enabled = enable;
		}
	}
}