using Attributes;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Toggle MonoBehaviour")]
	public sealed class InteractableMonoBehaviour : Interactable
	{
		[CanBeNullInPrefab, SerializeField] private MonoBehaviour target;
		[SerializeField] private bool enable;
		
		public override void Interact() => target.enabled = enable;
	}
}