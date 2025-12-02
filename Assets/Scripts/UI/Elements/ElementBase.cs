using Attributes;
using Runtime.Automation;
using UI.Layout.Elements;
using UnityEngine;

namespace UI.Elements
{
	public abstract class ElementBase : MonoBehaviour
	{
		[SerializeField] [AutoAssigned(AssignMode.Self, typeof(Element))]
		private Element layoutParent;

		public Element LayoutParent => layoutParent;
	}
}