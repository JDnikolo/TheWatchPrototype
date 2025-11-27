using UnityEditor;
using UnityEngine;

namespace Editor
{
	public abstract class DrawerBase : PropertyDrawer
	{
		private bool m_applyModifications;
		private bool m_markDirty;
		
		protected void ApplyModifications() => m_applyModifications = true;
		protected void MarkDirty() => m_markDirty = true;
		
		public sealed override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			OnGUIInternal(position, property, label);
			var serializedObject = property.serializedObject;
			if (m_applyModifications) serializedObject.ApplyModifiedProperties();
			if (m_markDirty) EditorUtility.SetDirty(serializedObject.targetObject);
		}
		
		protected abstract void OnGUIInternal(Rect position, SerializedProperty property, GUIContent label);
	}
}