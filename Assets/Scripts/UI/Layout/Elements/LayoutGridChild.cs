using UnityEngine;

namespace UI.Layout.Elements
{
	[AddComponentMenu("UI/Layout/Grid Designator")]
	public sealed class LayoutGridChild : BaseBehaviour
	{
		[SerializeField] private LayoutElement[] elements;
		
		public LayoutElement[] Elements => elements;
	}
}