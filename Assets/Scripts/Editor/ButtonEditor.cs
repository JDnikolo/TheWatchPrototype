using UI.Elements;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(Button), true)]
	[CanEditMultipleObjects]
	public class ButtonEditor : ParentEditor
	{
		private SerializedProperty m_anyClick;
		private SerializedProperty m_primaryReference;
		private SerializedProperty m_secondaryReference;
		private SerializedProperty m_onPrimaryClick;
		private SerializedProperty m_onSecondaryClick;

		protected override void OnEnable()
		{
			base.OnEnable();
			m_anyClick = serializedObject.FindProperty("anyClick");
			m_primaryReference = serializedObject.FindProperty("primaryReference");
			m_secondaryReference = serializedObject.FindProperty("secondaryReference");
			m_onPrimaryClick = serializedObject.FindProperty("onPrimaryClick");
			m_onSecondaryClick = serializedObject.FindProperty("onSecondaryClick");
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			m_anyClick = null;
			m_primaryReference = null;
			m_secondaryReference = null;
			m_onPrimaryClick = null;
			m_onSecondaryClick = null;
		}

		protected override void DisplayBeforeHidden()
		{
			base.DisplayBeforeHidden();
			EditorGUILayout.PropertyField(m_primaryReference);
			EditorGUILayout.PropertyField(m_onPrimaryClick);
			EditorGUILayout.PropertyField(m_anyClick);
			if (!m_anyClick.boolValue)
			{
				EditorGUILayout.PropertyField(m_secondaryReference);
				EditorGUILayout.PropertyField(m_onSecondaryClick);
			}
			else
			{
				if (m_secondaryReference.objectReferenceValue) m_secondaryReference.objectReferenceValue = null;
				if (m_onSecondaryClick.objectReferenceValue) m_onSecondaryClick.objectReferenceValue = null;
			}

			ApplyModifications();
		}
	}
}