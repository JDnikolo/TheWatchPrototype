using System;
using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(DeferredEditorAttribute))]
	public sealed class DeferredEditorDrawer : DrawerBase
	{
		protected override void OnGUIInternal(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType != SerializedPropertyType.ObjectReference)
				throw new NotSupportedException("[DeferredEditor] only supports UnityEngine.Object");
			var local = (DeferredEditorAttribute) attribute;
			EditorGUI.PropertyField(position, property, label);
			if (property.objectReferenceValue)
				using (new EditorGUI.DisabledScope(local.Disabled))
					UnityEditor.Editor.CreateEditor(property.objectReferenceValue).OnInspectorGUI();
			ApplyModifications();
		}
	}
}