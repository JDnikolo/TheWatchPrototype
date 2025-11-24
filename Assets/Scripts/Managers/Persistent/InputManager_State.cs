using Runtime.Automation;
using UnityEditor;

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
			void IEditorDisplayable.DisplayInEditor()
			{
				EditorGUILayout.IntField("Active Controls", ActiveControls);
				EditorGUILayout.Toggle("Cursor Visible", CursorVisible);
			}
#endif
		}
	}
}