using UI.Elements;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(ElementBase), true)]
	[CanEditMultipleObjects]
	public class ElementBaseEditor : EditorBase
	{
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;
		
		protected override void OnInspectorGUIInternal()
		{
			DisplayBeforeHidden();
			using (new EditorGUI.DisabledScope(true)) DisplayHidden();
		}

		protected virtual void DisplayBeforeHidden()
		{
		}
		
		protected virtual void DisplayHidden()
		{
		}
	}
}