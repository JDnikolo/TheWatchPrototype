using System;
using Attributes;
using Runtime.Automation;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(AutoAssignedAttribute))]
	public class AutoAssignedDrawer : DrawerBase
	{
		protected override void OnGUIInternal(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType != SerializedPropertyType.ObjectReference)
				throw new NotSupportedException("[" + nameof(AutoAssignedAttribute) +
												"] only supports UnityEngine.Object fields.");
			if (property.serializedObject.targetObject is not Component targetComponent) 
				throw new Exception("[" + nameof(AutoAssignedAttribute) +
									"] only supports UnityEngine.Component parents.");
			var local = (AutoAssignedAttribute) attribute;
			bool makeHidden;
			if (!property.objectReferenceValue)
			{
				Component component = null;
				if ((local.AssignMode & AssignMode.Self) != 0)
				{
					component = targetComponent.GetComponent(local.FieldType);
					if (component) goto Found;
				}
				
				if ((local.AssignMode & AssignMode.Parent) != 0)
				{
					component = targetComponent.GetComponentInParent(local.FieldType);
					if (component) goto Found;
				}

				if ((local.AssignMode & AssignMode.Child) != 0)
				{
					component = targetComponent.GetComponentInChildren(local.FieldType);
					if (component) goto Found;
				}
				
				Found:
				if (component)
				{
					makeHidden = true;
					property.objectReferenceValue = component;
					ApplyModifications();
				}
				else makeHidden = false;
			}
			else
				makeHidden = property.objectReferenceValue is Component referenceComponent &&
							((local.AssignMode & AssignMode.Self) == 0 ||
							referenceComponent.gameObject == targetComponent.gameObject) &&
							((local.AssignMode & AssignMode.Parent) == 0 ||
							referenceComponent.transform == targetComponent.transform.parent) &&
							((local.AssignMode & AssignMode.Child) == 0 ||
							referenceComponent.transform.parent == targetComponent.transform);

			using (new EditorGUI.DisabledScope(makeHidden)) EditorGUI.PropertyField(position, property, label, true);
		}
	}
}