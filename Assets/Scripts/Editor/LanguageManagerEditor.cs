using Managers.Persistent;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(LanguageManager))]
	public class LanguageManagerEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (target is LanguageManager local)
			{
				EditorGUILayout.EnumPopup("Current Language", local.CurrentLanguage);
				local.Localizers.DisplayObjectCollection("Active Localizers");
			}
		}
	}
}