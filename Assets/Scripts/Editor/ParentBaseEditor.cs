using UI.Layout;
using UI.Layout.Elements;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(ParentBase), true)]
	[CanEditMultipleObjects]
	public class ParentBaseEditor : LayoutElementEditor
	{
		protected SerializedProperty m_parent;

		protected override void OnEnable()
		{
			base.OnEnable();
			m_parent = serializedObject.FindProperty("parent");
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			m_parent = null;
		}

		private bool m_displayHidden;

		protected override void OnInspectorGUIInternal()
		{
			m_displayHidden = true;
			if (!EditorApplication.isPlaying && m_parent.objectReferenceValue is not ILayoutParent)
				m_displayHidden = false;
			base.OnInspectorGUIInternal();
		}

		protected override void DisplayBeforeHidden()
		{
			base.DisplayBeforeHidden();
			if (!m_displayHidden)
			{
				EditorGUILayout.PropertyField(m_parent);
				ApplyModifications();
			}
		}

		protected override void DisplayHiddenEditor()
		{
			base.DisplayHiddenEditor();
			if (m_displayHidden) EditorGUILayout.PropertyField(m_parent);
		}
	}
}