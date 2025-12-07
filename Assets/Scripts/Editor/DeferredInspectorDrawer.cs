using System;
using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(DeferredInspector))]
	public sealed class DeferredInspectorDrawer : DrawerBase
	{
		protected override void OnGUIInternal(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType != SerializedPropertyType.ObjectReference)
				throw new NotSupportedException("[" + nameof(DeferredInspector) +
												"] only supports UnityEngine.Object");
			EditorGUI.PropertyField(position, property, label);
			if (property.objectReferenceValue &&
				(property.serializedObject.targetObject is not Component targetComponent ||
				property.objectReferenceValue is not Component referenceComponent ||
				targetComponent.gameObject != referenceComponent.gameObject))
				UnityEditor.Editor.CreateEditor(property.objectReferenceValue).OnInspectorGUI();
			ApplyModifications();
		}
	}
}