using Runtime.Automation;
using UnityEditor;
using Utilities;

namespace Managers.Persistent
{
	public sealed partial class InputManager
	{
		public struct State
#if UNITY_EDITOR
			: IEditorDisplayable
#endif
		{
			public int ActiveControls;
			public bool CursorVisible;
#if UNITY_EDITOR
			public void DisplayInEditor()
			{
				EditorGUILayout.IntField("Active Controls", ActiveControls);
				EditorGUILayout.Toggle("Cursor Visible", CursorVisible);
			}
			
			public void DisplayInEditor(string name) => name.DisplayIndented(DisplayInEditor);
#endif
		}
	}
}