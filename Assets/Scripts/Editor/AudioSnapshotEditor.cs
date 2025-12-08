using Audio;
using UnityEditor;
using UnityEngine;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(AudioSnapshot))]
	public sealed class AudioSnapshotEditor : EditorBase
	{
		private SerializedProperty m_groups;
		private SerializedProperty m_volumes;

		protected override void OnEnable()
		{
			base.OnEnable();
			m_groups = serializedObject.FindProperty("groups");
			m_volumes = serializedObject.FindProperty("volumes");
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			m_groups  = null;
			m_volumes = null;
		}

		private void OnDestroy() => OnDisable();

		protected override bool DrawDefault => false;

		protected override void OnInspectorGUIInternal()
		{
			if (m_groups.objectReferenceValue is not AudioGroups audioGroups)
			{
				EditorGUILayout.PropertyField(m_groups);
				ApplyModifications();
				return;
			}
			
			var groups = audioGroups.Groups;
			var length = groups.SafeCount();
			if (length > 0)
			{
				ApplyModifications();
				if (m_volumes.arraySize != length) m_volumes.arraySize = length;
				for (var i = 0; i < length; i++)
				{
					var volume = m_volumes.GetArrayElementAtIndex(i);
					EditorGUILayout.PropertyField(volume, new GUIContent(groups[i].name));
				}
			}
		}
	}
}