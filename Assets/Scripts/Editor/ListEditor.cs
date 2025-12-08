using UI;
using UI.Layout;
using UI.Layout.Elements;
using UnityEditor;
using UnityEngine;
using Utilities;
using Node = System.Collections.Generic.LinkedListNode<UI.Layout.ILayoutElement>;
using ListMode = UI.Layout.Elements.List.Mode;

namespace Editor
{
	[CustomEditor(typeof(List), true)]
	[CanEditMultipleObjects]
	public class ListEditor : ElementEditor
	{
		private ListMode m_mode;
		private Node m_selectedNode;
		
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;

		protected override void OnInspectorGUIInternal()
		{
			var local = (List) target;
			m_mode = local.ListMode;
			base.OnInspectorGUIInternal();
		}

		protected override DirectionFlags GetBlockedDirections()
		{
			DirectionFlags extraBlocks;
			if (m_mode == ListMode.Circular)
			{
				var local = (List) target;
				extraBlocks = local.BlockedDirections;
			} 
			else extraBlocks = DirectionFlags.None;
			
			return base.GetBlockedDirections() | extraBlocks;
		}

		protected override void DisplayBeforeHidden()
		{
			base.DisplayBeforeHidden();
			if (EditorApplication.isPlaying) return;
			if (m_mode == ListMode.Circular && m_parent.objectReferenceValue is ILayoutControllingParent controllingParent)
			{
				var local = (List) target;
				var localBlocks = local.BlockedDirections;
				if (localBlocks == controllingParent.BlockedDirections)
				{
					local.ListMode = default;
					Debug.LogWarning(
						"List cannot be circular if its controlling parent occupies the same axis! (this would lead to the list softlocking the layout)");
				}
				else
				{
					if ((localBlocks & DirectionFlags.Left) != 0)
						m_leftNeighbor.objectReferenceValue = null;
					if ((localBlocks & DirectionFlags.Right) != 0)
						m_rightNeighbor.objectReferenceValue = null;
					if ((localBlocks & DirectionFlags.Up) != 0)
						m_topNeighbor.objectReferenceValue = null;
					if ((localBlocks & DirectionFlags.Down) != 0)
						m_bottomNeighbor.objectReferenceValue = null;
				}
			}
		}

		protected override void DisplayHidden()
		{
			base.DisplayHidden();
			if (EditorApplication.isPlaying)
			{
				var local = (List) target;
				m_selectedNode = local.SelectedNode;
				local.Elements.DisplayCollection("Elements", true, CustomFilter);
				m_selectedNode = null;
			}
		}

		private bool CustomFilter(ILayoutElement element)
		{
			var label = "Element";
			if (Equals(m_selectedNode, element)) label += " [Selected]";
			element.Display(label);
			return true;
		}

		private bool Equals(Node node, ILayoutElement element) => node != null && ReferenceEquals(node.Value, element);
	}
}