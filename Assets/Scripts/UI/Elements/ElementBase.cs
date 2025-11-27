using UI.Layout.Elements;
using UnityEngine;
using Utilities;

namespace UI.Elements
{
	public abstract class ElementBase : MonoBehaviour
	{
		[SerializeField] [HideInInspector] private Element layoutParent;

		public Element LayoutParent => layoutParent;
#if UNITY_EDITOR
		protected virtual void OnValidate()
		{
			if (TryGetComponent(out Element newLayoutParent)) this.DirtyReplace(ref layoutParent, newLayoutParent);
		}
#endif
	}
}