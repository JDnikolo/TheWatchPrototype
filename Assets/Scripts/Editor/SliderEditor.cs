using UI.Elements;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(Slider))]
	[CanEditMultipleObjects]
	public sealed class SliderEditor : UnityEditor.Editor
	{
		private SerializedProperty m_wholeNumbers;
		private SerializedProperty m_lowerValue;
		private SerializedProperty m_upperValue;
		private SerializedProperty m_lowerValueInt;
		private SerializedProperty m_upperValueInt;
		private SerializedProperty m_speedMultiplier;
		
		private void OnEnable()
		{
			m_wholeNumbers = serializedObject.FindProperty("wholeNumbers");
			m_lowerValue = serializedObject.FindProperty("lowerValue");
			m_upperValue = serializedObject.FindProperty("upperValue");
			m_lowerValueInt = serializedObject.FindProperty("lowerValueInt");
			m_upperValueInt = serializedObject.FindProperty("upperValueInt");
			m_speedMultiplier = serializedObject.FindProperty("speedMultiplier");
		}

		private void OnDisable()
		{
			m_wholeNumbers = null;
			m_lowerValue = null;
			m_upperValue = null;
			m_lowerValueInt = null;
			m_upperValueInt = null;
			m_speedMultiplier = null;
		}
		
		private void OnDestroy() => OnDisable();
		
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (!m_wholeNumbers.boolValue)
			{
				EditorGUILayout.PropertyField(m_lowerValue);
				EditorGUILayout.PropertyField(m_upperValue);
				EditorGUILayout.PropertyField(m_speedMultiplier);
			}
			else
			{
				EditorGUILayout.PropertyField(m_lowerValueInt);
				EditorGUILayout.PropertyField(m_upperValueInt);
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}