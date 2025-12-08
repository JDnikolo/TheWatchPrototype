using UI.Layout;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(LayoutElement), true)]
	[CanEditMultipleObjects]
	public class LayoutElementEditor : EditorBase
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
				var local = (LayoutElement) target;
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