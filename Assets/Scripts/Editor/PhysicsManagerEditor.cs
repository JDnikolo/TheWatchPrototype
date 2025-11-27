using Managers.Persistent;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(PhysicsManager))]
	public class PhysicsManagerEditor : EditorBase
	{
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;
		
		protected override void OnInspectorGUIInternal()
		{
			var local = (PhysicsManager) target;
			if (EditorApplication.isPlaying)
				using (new EditorGUI.DisabledScope(true))
				{
					EditorGUILayout.Toggle("Require Physics", local.RequirePhysics);
					EditorGUILayout.Toggle("Require Physics 2D", local.RequirePhysics2D);
				}
		}
	}
}