using UI.Elements;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(Button))]
	[CanEditMultipleObjects]
	public sealed class ButtonEditor : UnityEditor.Editor
	{
		private SerializedProperty m_anyClick;
		private SerializedProperty m_onSecondaryClick;

		private void OnEnable()
		{
			m_anyClick = serializedObject.FindProperty("anyClick");
			m_onSecondaryClick = serializedObject.FindProperty("onSecondaryClick");
		}

		private void OnDisable()
		{
			m_anyClick = null;
			m_onSecondaryClick = null;
		}

		private void OnDestroy() => OnDisable();

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (!m_anyClick.boolValue) EditorGUILayout.PropertyField(m_onSecondaryClick);
			else if (m_onSecondaryClick.objectReferenceValue)
			{
				m_onSecondaryClick.objectReferenceValue = null;
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}