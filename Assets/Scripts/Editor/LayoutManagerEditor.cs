using Managers;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(LayoutManager))]
	public class LayoutManagerEditor : UnityEditor.Editor
	{
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (target is LayoutManager local)
			{
				local.ParentHierarchy.DisplayObjectCollection("Parent Hierarchy");
				local.CurrentElement.DisplayObject("Current Element");
				local.CurrentInput.DisplayObject("Current Input");
			}
		}
	}
}