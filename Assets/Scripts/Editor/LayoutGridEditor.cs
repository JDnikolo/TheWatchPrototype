using UI.Layout;
using UI.Layout.Elements;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(LayoutGrid))]
	public sealed class LayoutGridEditor : LayoutElementEditor
	{
		private ILayoutElement m_selectedElement;

		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;

		protected override void DisplayHidden()
		{
			base.DisplayHidden();
			if (EditorApplication.isPlaying)
			{
				var local = (LayoutGrid) target;
				m_selectedElement = local.SelectedElement;
				local.Elements.DisplayCollection("Elements", true, CustomFilter);
				m_selectedElement = null;
			}
		}

		private bool CustomFilter(ILayoutElement element)
		{
			var label = "Element";
			if (ReferenceEquals(element, m_selectedElement)) label += " [Selected]";
			element.Display(label);
			return true;
		}
	}
}