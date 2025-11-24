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
			if (target is CameraManager local)
			{
				local.Camera.DisplayObject("Current Camera");
			}
		}
	}
}