using Runtime.Automation;
using UnityEditor;

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
			void IEditorDisplayable.DisplayInEditor()
			{
				EditorGUILayout.Toggle("Can Open",  CanOpenJournal);
			}
#endif
		}
	}
}