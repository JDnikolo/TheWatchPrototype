using Attributes;
using UI.Layout.Elements;
using UnityEngine;

namespace UI.Elements
{
	public abstract class ElementBase : BaseBehaviour
	{
		[CanBeNull, SerializeField, AutoAssigned(AssignModeFlags.Self, typeof(LayoutElement))]
		private LayoutElement layoutParent;

		public LayoutElement LayoutParent => layoutParent;
	}
}