using UI.Elements;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomEditor(typeof(ListBase), true)]
	[CanEditMultipleObjects]
	public class ListBaseEditor : EditorBase
	{
		private SerializedProperty m_layoutParent;

		protected virtual void OnEnable() => m_layoutParent = serializedObject.FindProperty("layoutParent");

		protected virtual void OnDisable() => m_layoutParent = null;

		private bool m_displayHidden;

		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;
		
		protected override void OnInspectorGUIInternal()
		{
			var local = (ListBase) target;
			if (m_layoutParent.objectReferenceValue is Component component)
				m_displayHidden = component.gameObject == local.gameObject;
			DisplayBeforeHidden();
			using (new EditorGUI.DisabledScope(true)) DisplayHidden();
		}

		protected virtual void DisplayBeforeHidden()
		{
			if (!m_displayHidden)
			{
				EditorGUILayout.PropertyField(m_layoutParent);
				ApplyModifications();
			}
		}
		
		protected virtual void DisplayHidden()
		{
			if (m_displayHidden) EditorGUILayout.PropertyField(m_layoutParent);
		}
	}
}