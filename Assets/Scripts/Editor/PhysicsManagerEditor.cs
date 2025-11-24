using Managers.Persistent;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(PhysicsManager))]
	public class PhysicsManagerEditor : UnityEditor.Editor
	{
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;
		
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (target is PhysicsManager local)
			{
				EditorGUILayout.Toggle("Require Physics", local.RequirePhysics);
				EditorGUILayout.Toggle("Require Physics 2D", local.RequirePhysics2D);
			}
		}
	}
}