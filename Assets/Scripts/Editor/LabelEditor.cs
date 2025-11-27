using UI.Elements;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(Label))]
	public sealed class LabelEditor : EditorBase
	{
		private SerializedProperty m_textToDisplay;
		
		private void OnEnable() => m_textToDisplay = serializedObject.FindProperty("textToDisplay");

		private void OnDisable() => m_textToDisplay = null;

		private void OnDestroy() => OnDisable();

		protected override void OnInspectorGUIInternal()
		{
			var local = (Label) target;
			if (EditorApplication.isPlaying)
				using (new EditorGUI.DisabledScope(true))
				{
					m_textToDisplay.objectReferenceValue.Display("Original Text");
					local.TextToDisplay.Display("Text To Display");
				}
			else
			{
				EditorGUILayout.PropertyField(m_textToDisplay);
				ApplyModifications();
			}
		}
	}
}