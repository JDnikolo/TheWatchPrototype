using UI.Layout;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(LayoutControlled), true, isFallback = true)]
	[CanEditMultipleObjects]
	public sealed class LayoutControlledEditor : UnityEditor.Editor
	{
		private SerializedProperty m_parent;
		private SerializedProperty m_leftNeighbor;
		private SerializedProperty m_rightNeighbor;
		private SerializedProperty m_topNeighbor;
		private SerializedProperty m_bottomNeighbor;

		private void OnEnable()
		{
			m_parent = serializedObject.FindProperty("parent");
			m_leftNeighbor = serializedObject.FindProperty("leftNeighbor");
			m_rightNeighbor = serializedObject.FindProperty("rightNeighbor");
			m_topNeighbor = serializedObject.FindProperty("topNeighbor");
			m_bottomNeighbor = serializedObject.FindProperty("bottomNeighbor");
		}

		private void OnDisable()
		{
			m_parent = null;
			m_leftNeighbor = null;
			m_rightNeighbor = null;
			m_topNeighbor = null;
			m_bottomNeighbor = null;
		}
		
		private void OnDestroy() => OnDisable();

		private bool m_showHidden;
		
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			var blockedDirection = LayoutBlockedDirections.None;
			if (m_parent.objectReferenceValue is ILayoutControllingParent controllingParent) 
				blockedDirection = controllingParent.BlockedDirections;
			if (blockedDirection != LayoutBlockedDirections.None) 
				m_showHidden = EditorGUILayout.Toggle("Show Hidden", m_showHidden);
			if (m_showHidden) blockedDirection = LayoutBlockedDirections.None;
			if (!blockedDirection.HasFlag(LayoutBlockedDirections.Left)) 
				EditorGUILayout.PropertyField(m_leftNeighbor);
			if (!blockedDirection.HasFlag(LayoutBlockedDirections.Right))  
				EditorGUILayout.PropertyField(m_rightNeighbor);
			if (!blockedDirection.HasFlag(LayoutBlockedDirections.Up)) 
				EditorGUILayout.PropertyField(m_topNeighbor);
			if (!blockedDirection.HasFlag(LayoutBlockedDirections.Down))  
				EditorGUILayout.PropertyField(m_bottomNeighbor);
		}
	}
}