using Callbacks.Layout;
using Runtime;
using Runtime.Automation;
using UnityEngine;
using Utilities;

namespace UI.Layout
{
	public abstract class LayoutManaged : LayoutElement, IPrewarm
#if UNITY_EDITOR
		, IHierarchyChanged
#endif
	{
		[SerializeField] [HideInInspector] private LayoutElement parent;
		
		private ILayoutCallback m_callback;
		
		public sealed override ILayoutElement Parent { get; set; }
		
		public void SetCallback(ILayoutCallback callback) => m_callback = callback;

		public override void Select() => m_callback?.OnSelected();

		public override void Deselect() => m_callback?.OnDeselected();

		public virtual void OnPrewarm() => Parent = parent;

		protected virtual void OnDestroy() => SetCallback(null);
#if UNITY_EDITOR
		public void SetParent(LayoutElement newParent) => this.DirtyReplace(ref parent, newParent);

		protected virtual void OnValidate() => TestParent();

		public virtual void OnHierarchyChanged() => TestParent();

		private void TestParent()
		{
			if (parent && parent is ILayoutParent && !transform.IsChildOf(parent.transform)) SetParent(null);
		}
#endif
	}
}