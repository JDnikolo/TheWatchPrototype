using System;
using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(SelfAssignedAttribute))]
	public class SelfAssignedDrawer : DrawerBase
	{
		protected override void OnGUIInternal(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType != SerializedPropertyType.ObjectReference)
				throw new NotSupportedException("[SelfAssigned] only supports UnityEngine.Object");
			var local = (SelfAssignedAttribute) attribute;
			if (property.serializedObject.targetObject is Component targetComponent)
			{
				if (!property.objectReferenceValue)
				{
					if (targetComponent.TryGetComponent(local.FieldType, out var component))
					{
						property.objectReferenceValue = component;
						ApplyModifications();
					}
				}
				else
				{
					if (property.objectReferenceValue is not Component referenceComponent || 
						referenceComponent.gameObject != targetComponent.gameObject)
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