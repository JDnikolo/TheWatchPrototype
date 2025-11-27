using UI.Layout;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(LayoutControlled), true)]
	[CanEditMultipleObjects]
	public class LayoutControlledEditor : LayoutManagedEditor
	{
		private SerializedProperty m_leftNeighbor;
		private SerializedProperty m_rightNeighbor;
		private SerializedProperty m_topNeighbor;
		private SerializedProperty m_bottomNeighbor;

		protected override void OnEnable()
		{
			base.OnEnable();
			m_leftNeighbor = serializedObject.FindProperty("leftNeighbor");
			m_rightNeighbor = serializedObject.FindProperty("rightNeighbor");
			m_topNeighbor = serializedObject.FindProperty("topNeighbor");
			m_bottomNeighbor = serializedObject.FindProperty("bottomNeighbor");
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			m_leftNeighbor = null;
			m_rightNeighbor = null;
			m_topNeighbor = null;
			m_bottomNeighbor = null;
		}
		
		private LayoutBlockedDirections m_blockedDirections;

		protected override void DisplayBeforeHidden()
		{
			base.DisplayBeforeHidden();
			m_blockedDirections = LayoutBlockedDirections.None;
			if (Parent.objectReferenceValue is ILayoutControllingParent controllingParent) 
				m_blockedDirections = controllingParent.BlockedDirections;
			if (!EditorApplication.isPlaying)
			{
				if (!m_blockedDirections.HasFlag(LayoutBlockedDirections.Left)) 
					EditorGUILayout.PropertyField(m_leftNeighbor);
				if (!m_blockedDirections.HasFlag(LayoutBlockedDirections.Right))  
					EditorGUILayout.PropertyField(m_rightNeighbor);
				if (!m_blockedDirections.HasFlag(LayoutBlockedDirections.Up)) 
					EditorGUILayout.PropertyField(m_topNeighbor);
				if (!m_blockedDirections.HasFlag(LayoutBlockedDirections.Down))  
					EditorGUILayout.PropertyField(m_bottomNeighbor);
				ApplyModifications();
			}
		}
		
		protected override void DisplayHidden()
		{
			base.DisplayHidden();
			if (EditorApplication.isPlaying)
			{
				var local = (LayoutControlled) target;
				local.LeftNeighbor.Display("Left Neighbor");
				local.RightNeighbor.Display("Right Neighbor");
				local.TopNeighbor.Display("Top Neighbor");
				local.BottomNeighbor.Display("Bottom Neighbor");
			}
			else
			{
				if (m_blockedDirections.HasFlag(LayoutBlockedDirections.Left)) 
					EditorGUILayout.PropertyField(m_leftNeighbor);
				if (m_blockedDirections.HasFlag(LayoutBlockedDirections.Right))  
					EditorGUILayout.PropertyField(m_rightNeighbor);
				if (m_blockedDirections.HasFlag(LayoutBlockedDirections.Up)) 
					EditorGUILayout.PropertyField(m_topNeighbor);
				if (m_blockedDirections.HasFlag(LayoutBlockedDirections.Down))  
					EditorGUILayout.PropertyField(m_bottomNeighbor);
			}
		}
	}
}