using UI.Layout.Elements;
using UnityEditor;
using UnityEngine;

namespace UI.Elements
{
	public abstract class ElementBase : MonoBehaviour
	{
		[SerializeField] private Element layoutParent;
#if UNITY_EDITOR
		public void SetLayoutParent(Element newLayoutParent)
		{
			if (layoutParent == newLayoutParent) return;
			layoutParent = newLayoutParent;
			EditorUtility.SetDirty(this);
		}
#endif
		protected Element LayoutParent => layoutParent;
	}
}