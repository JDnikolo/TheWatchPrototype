using Managers;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(JournalManager))]
	public class JournalManagerEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (target is JournalManager local)
			{
				EditorGUILayout.Toggle("Can Open", local.CanOpenJournal);
			}
		}
	}
}