using Managers.Persistent;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(SettingsManager))]
	public class SettingsManagerEditor : EditorBase
	{
		protected override void OnInspectorGUIInternal()
		{
			if (EditorApplication.isPlaying)
				using (new EditorGUI.DisabledScope(true))
					EditorGUILayout.TextField("Settings File Path", SettingsManager.FilePath);
		}
	}
}