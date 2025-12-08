using UI.Button;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(ControlButton))]
	public sealed class ControlButtonEditor : ParentEditor
	{
		private SerializedProperty m_parent;
		private SerializedProperty m_label;
		private SerializedProperty m_text;
		private SerializedProperty m_target;
		private SerializedProperty m_secondary;

		protected override void OnEnable()
		{
			base.OnEnable();
			m_parent = serializedObject.FindProperty("parent");
			m_label = serializedObject.FindProperty("label");
			m_text = serializedObject.FindProperty("text");
			m_target = serializedObject.FindProperty("target");
			m_secondary = serializedObject.FindProperty("secondary");
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			m_parent = null;
			m_label = null;
			m_text = null;
			m_target = null;
			m_secondary = null;
		}

		private bool m_hidden;
		
		protected override void OnInspectorGUIInternal()
		{
			var local = (ControlButton) target;
			m_hidden = local.transform.TryGetParent(out var parent) && parent.GetComponent<ControlButtonDouble>();
			if (m_hidden)
			{
				if (m_label.objectReferenceValue) m_label.objectReferenceValue = null;
				if (m_text.objectReferenceValue) m_text.objectReferenceValue = null;
			}
			
			ApplyModifications();
			base.OnInspectorGUIInternal();
		}

		protected override void DisplayBeforeHidden()
		{
			base.DisplayBeforeHidden();
			if (!m_hidden)
			{
				DisplayFields();
				EditorGUILayout.PropertyField(m_label);
				EditorGUILayout.PropertyField(m_text);
			}
		}

		protected override void DisplayHidden()
		{
			base.DisplayHidden();
			if (m_hidden)
			{
				EditorGUILayout.PropertyField(m_parent);
				DisplayFields();
			}
		}

		private void DisplayFields()
		{
			EditorGUILayout.PropertyField(m_target);
			EditorGUILayout.PropertyField(m_secondary);
		}
	}
}