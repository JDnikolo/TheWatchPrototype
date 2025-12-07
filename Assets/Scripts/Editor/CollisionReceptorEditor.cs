using Physics;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(CollisionReceptor))]
	public sealed class CollisionReceptorEditor : EditorBase
	{
		protected override void OnInspectorGUIInternal()
		{
			if (!EditorApplication.isPlaying) return;
			var local = (CollisionReceptor) target;
			using (new EditorGUI.DisabledScope(true))
			{
				local.OnCollisionEnterEditor.DisplayCollection("On Collision Enter");
				local.OnCollisionExitEditor.DisplayCollection("On Collision Exit");
				local.OnTriggerEnterEditor.DisplayCollection("On Trigger Enter");
				local.OnTriggerExitEditor.DisplayCollection("On Trigger Exit");
			}
		}
	}
}