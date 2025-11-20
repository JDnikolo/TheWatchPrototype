using UnityEngine;

namespace UI.Layout
{
	public abstract class LayoutElement : MonoBehaviour, ILayoutElement
	{
		public abstract ILayoutElement Parent { get; set; }
		
		public abstract void Select();

		public abstract void Deselect();
	}
}