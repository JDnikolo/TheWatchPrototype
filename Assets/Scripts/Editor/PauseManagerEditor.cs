using Managers;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(PauseManager))]
	public class PauseManagerEditor : UnityEditor.Editor
	{
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;
		
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (target is PauseManager local)
			{
				EditorGUILayout.Toggle("Can Pause", local.CanPause);
				local.PauseState.DisplayInEditor("Pause State");
			}
		}
	}
}