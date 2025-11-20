using Managers;
using UI.Layout;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Layout Element Interactable")]
	public sealed class InteractableLayoutElement : Interactable
	{
		[SerializeField] private LayoutElement target;
		
		public override void Interact()
		{
			var layoutManager = LayoutManager.Instance;
			if (layoutManager) layoutManager.Select(target);
		}
	}
}