using Managers.Persistent;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(SettingsManager))]
	public class SettingsManagerEditor : EditorBase
	{
		protected override void OnInspectorGUIInternal()
		{
			if (!EditorApplication.isPlaying) return;
			var local = (SettingsManager) target;
			using (new EditorGUI.DisabledScope(true)) local.SavedValues.DisplayDictionary("Saved Values");
		}
	}
}