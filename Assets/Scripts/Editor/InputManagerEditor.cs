using Managers.Persistent;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(InputManager))]
	public class InputManagerEditor : EditorBase
	{
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;
		
		protected override void OnInspectorGUIInternal()
		{
			var local = (InputManager) target;
			if (EditorApplication.isPlaying)
				using (new EditorGUI.DisabledScope(true))
				{
					EditorGUILayout.EnumPopup("Control Scheme", local.ControlScheme);
					DisplayInputMap(local.PlayerMap, "Player Map");
					DisplayInputMap(local.UIMap, "UI Map");
					DisplayInputMap(local.PersistentGameMap, "Persistent Game Map");
				}
		}

		private void DisplayInputMap(InputManager.InputMap inputMap, string name)
		{
			if (inputMap == null) EditorGUILayout.TextField(name, "Null");
			else EditorGUILayout.Toggle(name, inputMap.EditorEnabled);
		}
	}
}