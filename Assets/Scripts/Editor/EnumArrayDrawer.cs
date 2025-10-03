using System;
using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(EnumArrayAttribute))]
	public class EnumArrayDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{
			string name;
			try
			{
				var arrayPosition = int.Parse(property.propertyPath.Split('[', ']')[1]);
				name = Enum.GetName(((EnumArrayAttribute) attribute).EnumType, arrayPosition);
				if (name is null or "ENUM_LENGTH") name = "UNKNOWN";
			}
			catch
			{
				name = "ERROR";
			}
			
			EditorGUI.ObjectField(rect, property, new GUIContent(name));
		}
	}
}