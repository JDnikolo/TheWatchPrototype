using Managers;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(HighlightManager))]
	public class HighlightManagerEditor : EditorBase
	{
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;

		protected override void OnInspectorGUIInternal()
		{
			var local = (HighlightManager) target;
			if (EditorApplication.isPlaying)
				using (new EditorGUI.DisabledScope(true))
				{
					local.RaycastTarget.Display("Raycast Target");
					local.Rigidbodies.DisplayDictionary("Monitored Targets");
				}
		}
	}
}