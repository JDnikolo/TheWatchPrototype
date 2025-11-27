using UI.ComboBox;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(ComboDataProvider), true)]
	[CanEditMultipleObjects]
	public class ComboDataProviderEditor : EditorBase
	{
		protected override void OnInspectorGUIInternal()
		{
			var local = (ComboDataProvider) target;
			if (EditorApplication.isPlaying)
				using (new EditorGUI.DisabledScope(true))
					local.DataPoints.DisplayReadOnlyCollection("Data Points");
		}
	}
}