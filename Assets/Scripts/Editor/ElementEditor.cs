using UI;
using UI.Layout;
using UI.Layout.Elements;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(Element), true)]
	[CanEditMultipleObjects]
	public class ElementEditor : ParentBaseEditor
	{
		protected SerializedProperty m_leftNeighbor;
		protected SerializedProperty m_rightNeighbor;
		protected SerializedProperty m_topNeighbor;
		protected SerializedProperty m_bottomNeighbor;
		
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
		
		private DirectionFlags m_blockedDirections;

		protected virtual DirectionFlags GetBlockedDirections() =>
			m_parent.objectReferenceValue is ILayoutControllingParent controllingParent
				? controllingParent.BlockedDirections
				: DirectionFlags.None;

		protected override void DisplayBeforeHidden()
		{
			base.DisplayBeforeHidden();
			m_blockedDirections = GetBlockedDirections();
			if (!EditorApplication.isPlaying)
			{
				if ((m_blockedDirections & DirectionFlags.Left) == 0) 
					EditorGUILayout.PropertyField(m_leftNeighbor);
				if ((m_blockedDirections & DirectionFlags.Right) == 0) 
					EditorGUILayout.PropertyField(m_rightNeighbor);
				if ((m_blockedDirections & DirectionFlags.Up) == 0) 
					EditorGUILayout.PropertyField(m_topNeighbor);
				if ((m_blockedDirections & DirectionFlags.Down) == 0) 
					EditorGUILayout.PropertyField(m_bottomNeighbor);
				ApplyModifications();
			}
		}

		protected override void DisplayHiddenEditor()
		{
			base.DisplayHiddenEditor();
			if ((m_blockedDirections & DirectionFlags.Left) != 0)
				EditorGUILayout.PropertyField(m_leftNeighbor);
			if ((m_blockedDirections & DirectionFlags.Right) != 0)
				EditorGUILayout.PropertyField(m_rightNeighbor);
			if ((m_blockedDirections & DirectionFlags.Up) != 0)
				EditorGUILayout.PropertyField(m_topNeighbor);
			if ((m_blockedDirections & DirectionFlags.Down) != 0)
				EditorGUILayout.PropertyField(m_bottomNeighbor);
		}
	}
}