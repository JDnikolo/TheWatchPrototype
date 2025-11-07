using System;
using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(EnumArrayAttribute))]
	public sealed class EnumArrayDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{
			var enumAttribute = (EnumArrayAttribute) attribute;
			string name;
			try
			{
				var arrayPosition = int.Parse(property.propertyPath.Split('[', ']')[1]);
				name = Enum.GetName(enumAttribute.EnumType, arrayPosition);
				if (name is null or "ENUM_LENGTH") name = "UNKNOWN";
			}
			catch
			{
				name = "ERROR";
			}
			
			EditorGUI.PropertyField(rect, property, new GUIContent(name));
		}
	}
}