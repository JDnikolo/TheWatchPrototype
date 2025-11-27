using Managers.Persistent;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(CameraManager))]
	public class CameraManagerEditor : UnityEditor.Editor
	{
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			var local = (CameraManager) target;
			if (EditorApplication.isPlaying)
				using (new EditorGUI.DisabledScope(true))
					local.Camera.Display("Current Camera");
		}
	}
}