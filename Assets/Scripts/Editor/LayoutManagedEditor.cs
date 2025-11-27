using UI.Layout;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(LayoutManaged), true)]
	[CanEditMultipleObjects]
	public class LayoutManagedEditor : EditorBase
	{
		protected SerializedProperty Parent { get; private set; }

		protected virtual void OnEnable() => Parent = serializedObject.FindProperty("parent");

		protected virtual void OnDisable() => Parent = null;
		
		private bool m_displayHidden;

		protected override void OnInspectorGUIInternal()
		{
			m_displayHidden = true;
			if (!EditorApplication.isPlaying && Parent.objectReferenceValue is not ILayoutParent)
				m_displayHidden = false;
			DisplayBeforeHidden();
			using (new EditorGUI.DisabledScope(true)) DisplayHidden();
		}

		protected virtual void DisplayBeforeHidden()
		{
			if (!m_displayHidden)
			{
				EditorGUILayout.PropertyField(Parent);
				ApplyModifications();
			}
		}
		
		protected virtual void DisplayHidden()
		{
			if (EditorApplication.isPlaying)
			{
				var local = (LayoutManaged) target;
				local.Parent.Display("Parent");
			}
			else if (m_displayHidden) EditorGUILayout.PropertyField(Parent);
		}
	}
}