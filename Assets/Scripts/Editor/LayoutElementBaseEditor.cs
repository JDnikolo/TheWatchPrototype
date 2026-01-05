using UI.Layout;
using UI.Layout.Elements;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(LayoutElementBase), true)]
	[CanEditMultipleObjects]
	public class LayoutElementBaseEditor : EditorBase
	{
		protected override void OnInspectorGUIInternal()
		{
			DisplayBeforeHidden();
			using (new EditorGUI.DisabledScope(true)) DisplayHidden();
		}
		
		protected virtual void DisplayBeforeHidden()
		{
		}

		protected virtual void DisplayHidden()
		{
			if (EditorApplication.isPlaying)
			{
				var local = (LayoutElementBase) target;
				local.Parent.Display("Parent");
				local.LeftNeighbor.Display("Left Neighbor");
				local.RightNeighbor.Display("Right Neighbor");
				local.TopNeighbor.Display("Top Neighbor");
				local.BottomNeighbor.Display("Bottom Neighbor");
			}
			else DisplayHiddenEditor();
		}

		protected virtual void DisplayHiddenEditor()
		{
		}
	}
}