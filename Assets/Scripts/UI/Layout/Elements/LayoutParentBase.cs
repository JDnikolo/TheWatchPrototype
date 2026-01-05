using Attributes;
using Callbacks.Prewarm;
using Runtime.Automation;
using UnityEngine;
using Utilities;

namespace UI.Layout.Elements
{
	public abstract class LayoutParentBase : LayoutElementBase, IPrewarm
#if UNITY_EDITOR
		, IHierarchyChanged
#endif
	{
		[CanBeNull, SerializeField, HideInInspector]
		private LayoutElementBase parent;
		
		public sealed override ILayoutElement Parent { get; set; }
		
		public virtual void OnPrewarm() => Parent = parent;
#if UNITY_EDITOR
		public LayoutElementBase ManagedParent
		{
			get => parent;
			set => this.DirtyReplaceObject(ref parent, value);
		}
		
		protected virtual void OnValidate()
		{
			if (parent && (parent == this || parent is ILayoutParent && 
					!transform.IsChildOf(parent.transform))) 
				ManagedParent = null;
		}
		
		public virtual void OnHierarchyChanged() => OnValidate();
#endif
	}
}