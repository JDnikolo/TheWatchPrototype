using UI.Layout.Elements;
using UnityEngine;
using Utilities;

namespace UI.Elements
{
	public abstract class ListBase : BaseBehaviour
	{
		[SerializeField] [HideInInspector] private LayoutList layoutParent;

		public LayoutList LayoutParent => layoutParent;
#if UNITY_EDITOR
		protected virtual void OnValidate()
		{
			if (TryGetComponent(out LayoutList newLayoutParent)) this.DirtyReplaceObject(ref layoutParent, newLayoutParent);
		}
#endif
	}
}