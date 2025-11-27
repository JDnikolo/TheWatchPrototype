using Managers;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(PauseManager))]
	public class PauseManagerEditor : EditorBase
	{
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;
		
		protected override void OnInspectorGUIInternal()
		{
			var local = (PauseManager) target;
			if (EditorApplication.isPlaying)
				using (new EditorGUI.DisabledScope(true))
				{
					EditorGUILayout.Toggle("Can Pause", local.CanPause);
					local.PauseState.DisplayInEditor("Pause State");
				}
		}
	}
}