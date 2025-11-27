using System;
using Attributes;
using UnityEditor;
using UnityEngine;
using Utilities;

namespace Editor
{
	[CustomPropertyDrawer(typeof(ParentAssignedAttribute))]
	public class ParentAssignedDrawer : DrawerBase
	{
		protected override void OnGUIInternal(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType != SerializedPropertyType.ObjectReference)
				throw new NotSupportedException("[ParentAssigned] only supports UnityEngine.Object");
			var local = (ParentAssignedAttribute) attribute;
			if (property.serializedObject.targetObject is Component targetComponent)
			{
				if (!property.objectReferenceValue)
				{
					if (targetComponent.GetParent(out var parent) && 
						parent.TryGetComponent(local.FieldType, out var component))
					{
						property.objectReferenceValue = component;
						ApplyModifications();
					}
				}
				else
				{
					if (property.objectReferenceValue is not Component referenceComponent || 
						referenceComponent.transform != targetComponent.transform.parent)
					{
						property.objectReferenceValue = null;
						ApplyModifications();
					}
				}

				using (new EditorGUI.DisabledScope(true)) EditorGUI.PropertyField(position, property, label, true);
			}
		}
	}
}