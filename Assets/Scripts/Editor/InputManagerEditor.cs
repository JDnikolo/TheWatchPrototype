using Managers.Persistent;
using UnityEditor;
using Utilities;

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
					EditorGUILayout.EnumPopup("Control Scheme", local.ControlSchemeEditor);
					EditorGUILayout.IntField("Active Controls", local.ActiveControls);
					EditorGUILayout.IntField("Active Specials", local.ActiveSpecials);
					DisplayInputMap(local.PlayerMap, "Player Map");
					DisplayInputMap(local.UIMap, "UI Map");
					DisplaySpecialInput(local.BackSpecial, "Back Special");
				}
		}

		private void DisplayInputMap(InputManager.InputMap inputMap, string name)
		{
			if (inputMap == null) EditorGUILayout.TextField(name, Utils.NULL_STRING);
			else EditorGUILayout.Toggle(name, inputMap.EditorEnabled);
		}

		private void DisplaySpecialInput<T>(InputManager.SpecialInput<T> input, string name)
		{
			EditorGUILayout.Toggle(name, input.Enabled);
			EditorGUILayout.EnumPopup("State", input.Action.GetInputState());
			input.Hooks.DisplayCollection("Hooks");
		}
	}
}