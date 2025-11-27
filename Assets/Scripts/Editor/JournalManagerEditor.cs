using Managers;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(JournalManager))]
	public class JournalManagerEditor : EditorBase
	{
		protected override void OnInspectorGUIInternal()
		{
			var local = (JournalManager) target;
			if (EditorApplication.isPlaying)
				using (new EditorGUI.DisabledScope(true))
					EditorGUILayout.Toggle("Can Open", local.CanOpenJournal);
		}
	}
}