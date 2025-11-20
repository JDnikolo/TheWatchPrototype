using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(ToggleHiddenAttribute))]
	public class ToggleHiddenDrawer : PropertyDrawer
	{
		private bool m_showHidden;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			//m_showHidden = EditorGUILayout.Toggle(label, m_showHidden);
			//if (m_showHidden) EditorGUI.PropertyField(position, property, label);
		}
	}
}