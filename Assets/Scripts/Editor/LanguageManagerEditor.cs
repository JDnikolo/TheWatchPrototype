using Managers.Persistent;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(LanguageManager))]
	public class LanguageManagerEditor : EditorBase
	{
		protected override void OnInspectorGUIInternal()
		{
			var local = (LanguageManager) target;
			if (EditorApplication.isPlaying)
				using (new EditorGUI.DisabledScope(true))
				{
					EditorGUILayout.EnumPopup("Language", local.Language);
					local.Localizers.DisplayCollection("Active Localizers");
				}
		}
	}
}