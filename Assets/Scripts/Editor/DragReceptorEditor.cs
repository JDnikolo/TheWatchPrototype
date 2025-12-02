using UI;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(DragReceptor))]
	public sealed class DragReceptorEditor : EditorBase
	{
		protected override void OnInspectorGUIInternal()
		{
			if (!EditorApplication.isPlaying) return;
			var local = (DragReceptor) target;
			using (new EditorGUI.DisabledScope(true))
			{
				local.BeginDragCallbacks.DisplayCollection("Begin Drag Callbacks");
				local.DragCallbacks.DisplayCollection("Drag Callbacks");
				local.EndDragCallbacks.DisplayCollection("End Drag Callbacks");
			}
		}
	}
}