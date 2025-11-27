using Runtime.Automation;
using UnityEditor;
using Utilities;

namespace Managers
{
	public sealed partial class JournalManager
	{
		public struct State
#if UNITY_EDITOR
			: IEditorDisplayable
#endif
		{
			public bool CanOpenJournal;
#if UNITY_EDITOR
			public void DisplayInEditor() => EditorGUILayout.Toggle("Can Open",  CanOpenJournal);

			public void DisplayInEditor(string name) => name.DisplayIndented(DisplayInEditor);
#endif
		}
	}
}