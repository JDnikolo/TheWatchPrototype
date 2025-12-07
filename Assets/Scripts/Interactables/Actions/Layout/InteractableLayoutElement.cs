using Managers;
using UI.Layout;
using UnityEngine;

namespace Interactables.Actions.Layout
{
	[AddComponentMenu("Interactables/Layout/Select Layout Element")]
	public sealed class InteractableLayoutElement : Interactable
	{
		[SerializeField] private LayoutElement target;
		
		public override void Interact() => LayoutManager.Instance.Select(target);
	}
}