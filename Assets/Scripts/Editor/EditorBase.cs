using UnityEditor;

namespace Editor
{
	public abstract class EditorBase : UnityEditor.Editor
	{
		private bool m_applyModifications;
		private bool m_markDirty;
		
		protected void ApplyModifications() => m_applyModifications = true;
		protected void MarkDirty() => m_markDirty = true;

		public sealed override void OnInspectorGUI()
		{
			m_applyModifications = false;
			m_markDirty = false;
			DrawDefaultInspector();
			OnInspectorGUIInternal();
			if (m_applyModifications) serializedObject.ApplyModifiedProperties();
			if (m_markDirty) EditorUtility.SetDirty(serializedObject.targetObject);
		}

		protected abstract void OnInspectorGUIInternal();
	}
}