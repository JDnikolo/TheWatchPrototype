using Runtime.Automation;
using Runtime.FixedUpdate;
using Runtime.FrameUpdate;
using Runtime.LateUpdate;
using UnityEditor;
using Utilities;

namespace Managers.Persistent
{
	public sealed partial class GameManager
	{
		public struct State
#if UNITY_EDITOR
			: IEditorDisplayable
#endif
		{
			internal FrameUpdatePosition FrameUpdatePosition;
			internal LateUpdatePosition LateUpdatePosition;
			internal FixedUpdatePosition FixedUpdatePosition;
#if UNITY_EDITOR
			public void DisplayInEditor()
			{
				EditorGUILayout.EnumPopup("Frame Update Invoke", FrameUpdatePosition);
				EditorGUILayout.EnumPopup("Late Update Invoke", LateUpdatePosition);
				EditorGUILayout.EnumPopup("Fixed Update Invoke", FixedUpdatePosition);
			}
			
			public void DisplayInEditor(string name) => name.DisplayIndented(DisplayInEditor);
#endif
		}
	}
}