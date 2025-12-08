using UI.Elements;
using UnityEditor;
using UnityEngine;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(Label))]
	public sealed class LabelEditor : EditorBase
	{
		private SerializedProperty m_textToDisplay;
		
		protected override void OnEnable()
		{
			base.OnEnable();
			m_textToDisplay = serializedObject.FindProperty("textToDisplay");
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			m_textToDisplay = null;
		}

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