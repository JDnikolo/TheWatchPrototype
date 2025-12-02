using Managers;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(LayoutManager))]
	public class LayoutManagerEditor : EditorBase
	{
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;

		protected override void OnInspectorGUIInternal()
		{
			var local = (LayoutManager) target;
			if (EditorApplication.isPlaying)
				using (new EditorGUI.DisabledScope(true))
				{
					local.ParentHierarchy.DisplayCollection("Parent Hierarchy");
					local.CurrentElement.Display("Current Element");
				}
		}
	}
}