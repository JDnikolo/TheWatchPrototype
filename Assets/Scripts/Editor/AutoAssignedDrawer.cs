using System;
using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(AutoAssigned))]
	public class AutoAssignedDrawer : DrawerBase
	{
		protected override void OnGUIInternal(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType != SerializedPropertyType.ObjectReference)
				throw new NotSupportedException("[" + nameof(AutoAssigned) +
												"] only supports UnityEngine.Object fields.");
			if (property.serializedObject.targetObject is not Component targetComponent) 
				throw new Exception("[" + nameof(AutoAssigned) +
									"] only supports UnityEngine.Component parents.");
			var targetTransform = targetComponent.transform;
			var local = (AutoAssigned) attribute;
			bool makeHidden;
			if (property.objectReferenceValue is Component referenceComponent)
			{
				var referenceTransform = referenceComponent.transform;
				makeHidden =
					(local.AssignMode & AssignMode.Self) != 0 && referenceComponent.gameObject == targetComponent.gameObject ||
					(local.AssignMode & AssignMode.Parent) != 0 && referenceTransform == targetTransform.parent ||
					(local.AssignMode & AssignMode.Child) != 0 && referenceTransform.parent == targetTransform;

				if (!makeHidden && !EditorApplication.isPlaying && GUILayout.Button("Force"))
				{
					property.objectReferenceValue = null;
					ApplyModifications();
				}
			}
			else makeHidden = false;
			
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
					var parent = targetComponent.transform.parent;
					if (parent) component = parent.GetComponent(local.FieldType);
					if (component) goto Found;
				}

				if ((local.AssignMode & AssignMode.Child) != 0)
				{
					var childCount = targetTransform.childCount;
					for (var i = 0; i < childCount; i++)
					{
						component = targetTransform.GetChild(i).GetComponent(local.FieldType);
						if (component) goto Found;
					}
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

			using (new EditorGUI.DisabledScope(makeHidden)) EditorGUI.PropertyField(position, property, label, true);
		}
	}
}