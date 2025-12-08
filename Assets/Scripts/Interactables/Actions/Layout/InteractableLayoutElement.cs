using Managers;
using UI.Layout;
using UnityEngine;
using Utilities;

namespace Interactables.Actions.Layout
{
	[AddComponentMenu("Interactables/Layout/Select Layout Element")]
	public sealed class InteractableLayoutElement : Interactable
	{
		[SerializeField] private LayoutElement target;
		
		public override void Interact() => LayoutManager.Instance.Select(target);
#if UNITY_EDITOR
		public LayoutElement Target
		{
			get => target;
			set => this.DirtyReplaceObject(ref target, value);
		}
#endif
	}
}