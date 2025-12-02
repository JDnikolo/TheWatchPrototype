using UI.Layout;
using UI.Layout.Elements;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(Element), true)]
	[CanEditMultipleObjects]
	public class ElementEditor : EditorBase
	{
		protected SerializedProperty m_parent;
		protected SerializedProperty m_leftNeighbor;
		protected SerializedProperty m_rightNeighbor;
		protected SerializedProperty m_topNeighbor;
		protected SerializedProperty m_bottomNeighbor;
		
		protected virtual void OnEnable()
		{
			m_parent = serializedObject.FindProperty("parent");
			m_leftNeighbor = serializedObject.FindProperty("leftNeighbor");
			m_rightNeighbor = serializedObject.FindProperty("rightNeighbor");
			m_topNeighbor = serializedObject.FindProperty("topNeighbor");
			m_bottomNeighbor = serializedObject.FindProperty("bottomNeighbor");
		}

		protected virtual void OnDisable()
		{
			m_parent = null;
			m_leftNeighbor = null;
			m_rightNeighbor = null;
			m_topNeighbor = null;
			m_bottomNeighbor = null;
		}

		private bool m_displayHidden;

		protected override void OnInspectorGUIInternal()
		{
			m_displayHidden = true;
			if (!EditorApplication.isPlaying && m_parent.objectReferenceValue is not ILayoutParent)
				m_displayHidden = false;
			DisplayBeforeHidden();
			using (new EditorGUI.DisabledScope(true)) DisplayHidden();
		}
		
		private LayoutBlockedDirections m_blockedDirections;

		protected virtual LayoutBlockedDirections GetBlockedDirections() =>
			m_parent.objectReferenceValue is ILayoutControllingParent controllingParent
				? controllingParent.BlockedDirections
				: LayoutBlockedDirections.None;

		protected virtual void DisplayBeforeHidden()
		{
			if (!m_displayHidden) EditorGUILayout.PropertyField(m_parent);
			m_blockedDirections = GetBlockedDirections();
			if (!EditorApplication.isPlaying)
			{
				if ((m_blockedDirections & LayoutBlockedDirections.Left) == 0) 
					EditorGUILayout.PropertyField(m_leftNeighbor);
				if ((m_blockedDirections & LayoutBlockedDirections.Right) == 0) 
					EditorGUILayout.PropertyField(m_rightNeighbor);
				if ((m_blockedDirections & LayoutBlockedDirections.Up) == 0) 
					EditorGUILayout.PropertyField(m_topNeighbor);
				if ((m_blockedDirections & LayoutBlockedDirections.Down) == 0) 
					EditorGUILayout.PropertyField(m_bottomNeighbor);
				ApplyModifications();
			}
		}

		protected virtual void DisplayHidden()
		{
			if (EditorApplication.isPlaying)
			{
				var local = (Element) target;
				local.Parent.Display("Parent");
				local.LeftNeighbor.Display("Left Neighbor");
				local.RightNeighbor.Display("Right Neighbor");
				local.TopNeighbor.Display("Top Neighbor");
				local.BottomNeighbor.Display("Bottom Neighbor");
			}
			else
			{
				if (m_displayHidden) EditorGUILayout.PropertyField(m_parent);
				if ((m_blockedDirections & LayoutBlockedDirections.Left) != 0)
					EditorGUILayout.PropertyField(m_leftNeighbor);
				if ((m_blockedDirections & LayoutBlockedDirections.Right) != 0)
					EditorGUILayout.PropertyField(m_rightNeighbor);
				if ((m_blockedDirections & LayoutBlockedDirections.Up) != 0)
					EditorGUILayout.PropertyField(m_topNeighbor);
				if ((m_blockedDirections & LayoutBlockedDirections.Down) != 0)
					EditorGUILayout.PropertyField(m_bottomNeighbor);
			}
		}
	}
}