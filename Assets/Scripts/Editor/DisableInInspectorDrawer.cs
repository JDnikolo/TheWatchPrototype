using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(DisableInInspector))]
	public sealed class DisableInInspectorDrawer : DrawerBase
	{
		protected override void OnGUIInternal(Rect position, SerializedProperty property, GUIContent label)
		{
			var local = (DisableInInspector) attribute;
			bool disable;
			if (local.AllowInEditor) disable = EditorApplication.isPlaying;
			else disable = true;
			using (new EditorGUI.DisabledScope(disable)) EditorGUI.PropertyField(position, property, label);
		}
	}
}