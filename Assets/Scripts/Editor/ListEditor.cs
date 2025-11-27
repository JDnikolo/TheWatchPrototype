using UI.Layout;
using UI.Layout.Elements;
using UnityEditor;
using Utilities;
using Node = System.Collections.Generic.LinkedListNode<UI.Layout.ILayoutElement>;

namespace Editor
{
	[CustomEditor(typeof(List), true)]
	[CanEditMultipleObjects]
	public class ListEditor : LayoutControlledEditor
	{
		private Node m_firstNode;
		private Node m_lastNode;
		private Node m_selectedNode;
		
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;
		
		protected override void DisplayHidden()
		{
			base.DisplayHidden();
			if (EditorApplication.isPlaying)
			{
				var local = (List) target;
				m_firstNode = local.FirstConnectedNode;
				m_lastNode = local.LastConnectedNode;
				m_selectedNode = local.SelectedNode;
				local.Elements.DisplayCollection("Elements", true, CustomFilter);
				m_firstNode = null;
				m_lastNode = null;
				m_selectedNode = null;
			}
		}

		private bool CustomFilter(ILayoutElement element)
		{
			var label = "Element";
			if (Equals(m_firstNode, element)) label += " [First]";
			if (Equals(m_lastNode, element)) label += " [Last]";
			if (Equals(m_selectedNode, element)) label += " [Selected]";
			element.Display(label);
			return true;
		}

		private bool Equals(Node node, ILayoutElement element) => node != null && ReferenceEquals(node.Value, element);
	}
}