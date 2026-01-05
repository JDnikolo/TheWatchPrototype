using Managers;
using UI.Layout;
using UI.Layout.Elements;
using UnityEngine;
using Utilities;

namespace Interactables.Actions.Layout
{
	[AddComponentMenu("Interactables/Layout/Select Layout Element")]
	public sealed class InteractableLayoutElement : Interactable
	{
		[SerializeField] private LayoutElementBase target;
		
		public override void Interact() => LayoutManager.Instance.Select(target);
#if UNITY_EDITOR
		public LayoutElementBase Target
		{
			get => target;
			set => this.DirtyReplaceObject(ref target, value);
		}
#endif
	}
}