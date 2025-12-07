using UI.Elements;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(Button))]
	[CanEditMultipleObjects]
	public sealed class ButtonEditor : ButtonBaseEditor
	{
		private SerializedProperty m_anyClick;
		private SerializedProperty m_onPrimaryClick;
		private SerializedProperty m_onSecondaryClick;

		protected override void OnEnable()
		{
			base.OnEnable();
			m_anyClick = serializedObject.FindProperty("anyClick");
			m_onPrimaryClick = serializedObject.FindProperty("onPrimaryClick");
			m_onSecondaryClick = serializedObject.FindProperty("onSecondaryClick");
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			m_anyClick = null;
			m_onPrimaryClick = null;
			m_onSecondaryClick = null;
		}

		protected override void DisplayBeforeHidden()
		{
			base.DisplayBeforeHidden();
			EditorGUILayout.PropertyField(m_anyClick);
			EditorGUILayout.PropertyField(m_onPrimaryClick);
			if (!m_anyClick.boolValue) EditorGUILayout.PropertyField(m_onSecondaryClick);
			else if (m_onSecondaryClick.objectReferenceValue) m_onSecondaryClick.objectReferenceValue = null;
			ApplyModifications();
		}
	}
}