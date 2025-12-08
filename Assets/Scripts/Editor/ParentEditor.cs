using UI.Elements;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(Parent), true)]
	[CanEditMultipleObjects]
	public class ParentEditor : ElementBaseEditor
	{
		protected override void DisplayHidden()
		{
			base.DisplayHidden();
			var local = (Parent) target;
			if (EditorApplication.isPlaying)
			{
				EditorGUILayout.Toggle("Selected", local.Selected);
				EditorGUILayout.Toggle("Explicit", local.Explicit);
			}
		}
	}
}