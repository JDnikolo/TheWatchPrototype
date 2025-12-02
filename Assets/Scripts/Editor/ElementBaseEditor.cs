using UI.Elements;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(ElementBase), true)]
	[CanEditMultipleObjects]
	public class ElementBaseEditor : EditorBase
	{
		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

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