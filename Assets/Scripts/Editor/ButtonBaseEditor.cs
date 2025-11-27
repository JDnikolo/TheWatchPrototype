using UI.Elements;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(ButtonBase), true)]
	[CanEditMultipleObjects]
	public class ButtonBaseEditor : ElementBaseEditor
	{
		protected override void DisplayHidden()
		{
			base.DisplayHidden();
			var local = (ButtonBase) target;
			if (EditorApplication.isPlaying) EditorGUILayout.Toggle("Selected", local.Selected);
		}
	}
}