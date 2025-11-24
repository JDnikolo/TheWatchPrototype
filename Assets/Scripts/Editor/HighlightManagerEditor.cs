using Managers;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(HighlightManager))]
	public class HighlightManagerEditor : UnityEditor.Editor
	{
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;
		
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (target is HighlightManager local)
			{
				local.RaycastTarget.DisplayObject("Raycast Target");
				local.Rigidbodies.DisplayObjectDictionary("Monitored Targets");
			}
		}
	}
}