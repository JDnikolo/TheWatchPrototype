using UI;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(PointerReceptor))]
	public sealed class PointerReceptorEditor : EditorBase
	{
		protected override void OnInspectorGUIInternal()
		{
			if (!EditorApplication.isPlaying) return;
			var local = (PointerReceptor) target;
			using (new EditorGUI.DisabledScope(true))
			{
				local.EnterCallbacks.DisplayCollection("Pointer Enter Callbacks");
				local.ExitCallbacks.DisplayCollection("Pointer Exit Callbacks");
				local.DownCallbacks.DisplayCollection("Pointer Down Callbacks");
				local.UpCallbacks.DisplayCollection("Pointer Up Callbacks");
				local.ClickCallbacks.DisplayCollection("Pointer Click Callbacks");
			}
		}
	}
}