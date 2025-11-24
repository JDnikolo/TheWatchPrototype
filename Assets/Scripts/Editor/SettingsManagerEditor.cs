using Managers.Persistent;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(SettingsManager))]
	public class SettingsManagerEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (target is SettingsManager)
			{
				EditorGUILayout.TextField("Settings File Path", SettingsManager.FilePath);
			}
		}
	}
}