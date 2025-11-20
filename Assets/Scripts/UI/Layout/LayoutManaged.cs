using Interactables;
using Runtime;
using Runtime.Automation;
using UnityEditor;
using UnityEngine;

namespace UI.Layout
{
	public abstract class LayoutManaged : LayoutElement, IPrewarm
#if UNITY_EDITOR
		, IHierarchyChanged
#endif
	{
		[SerializeField] private LayoutElement parent;
		[SerializeField] private Interactable onSelected;
		[SerializeField] private Interactable onDeselected;
		
		public sealed override ILayoutElement Parent { get; set; }

		public override void Select()
		{
			if (onSelected) onSelected.Interact();
		}

		public override void Deselect()
		{
			if (onDeselected) onDeselected.Interact();
		}

		public virtual void OnPrewarm() => Parent = parent;
#if UNITY_EDITOR
		public void SetParent(LayoutElement newParent)
		{
			if (parent == newParent) return;
			parent = newParent;
			EditorUtility.SetDirty(this);
		}

		protected virtual void OnValidate() => TestParent();

		public virtual void OnHierarchyChanged() => TestParent();

		private void TestParent()
		{
			if (parent && parent is ILayoutControllingParent && !transform.IsChildOf(parent.transform)) SetParent(null);
		}
#endif
	}
}