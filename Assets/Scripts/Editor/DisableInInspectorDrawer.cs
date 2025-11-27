using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(DisableInInspectorAttribute))]
	public sealed class DisableInInspectorDrawer : DrawerBase
	{
		protected override void OnGUIInternal(Rect position, SerializedProperty property, GUIContent label)
		{
			using (new EditorGUI.DisabledScope(true)) EditorGUI.PropertyField(position, property, label);
		}
	}
}