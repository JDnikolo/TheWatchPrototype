using UI.Layout.Elements;
using UnityEngine;
using Utilities;

namespace UI.Elements
{
	public abstract class ListBase : BaseBehaviour
	{
		[SerializeField] [HideInInspector] private List layoutParent;

		public List LayoutParent => layoutParent;
#if UNITY_EDITOR
		protected virtual void OnValidate()
		{
			if (TryGetComponent(out List newLayoutParent)) this.DirtyReplaceObject(ref layoutParent, newLayoutParent);
		}
#endif
	}
}