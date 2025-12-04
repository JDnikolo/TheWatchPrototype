using Managers;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(PauseManager))]
	public class PauseManagerEditor : EditorBase
	{
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;
		
		protected override void OnInspectorGUIInternal()
		{
			if (!EditorApplication.isPlaying)
			{
				ApplyModifications();
				return;
			}
			
			var local = (PauseManager) target;
			using (new EditorGUI.DisabledScope(true))
			{
				EditorGUILayout.Toggle("Can Pause", local.CanPause);
				local.PauseState.DisplayInEditor("Pause State");
			}
		}
	}
}