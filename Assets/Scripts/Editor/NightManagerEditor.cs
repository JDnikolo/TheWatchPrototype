using Managers;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(NightManager))]
	public sealed class NightManagerEditor : EditorBase
	{
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;
		
		protected override void OnInspectorGUIInternal()
		{
			if (!EditorApplication.isPlaying)
			{
				ApplyModifications();
				return;
			}
			
			var local = (NightManager) target;
			using (new EditorGUI.DisabledScope(true))
			{
				local.CurrentTime.DisplayInEditor();
				local.ChangedCallbacks.DisplayCollection("Callbacks");
			}
		}
	}
}