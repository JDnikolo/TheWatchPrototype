using Attributes;
using UI.Layout.Elements;
using UnityEngine;

namespace UI.Elements
{
	public abstract class ElementBase : BaseBehaviour
	{
		[SerializeField] [AutoAssigned(AssignMode.Self, typeof(Element))]
		private Element layoutParent;

		public Element LayoutParent => layoutParent;
	}
}