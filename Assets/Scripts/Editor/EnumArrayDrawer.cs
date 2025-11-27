using System;
using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(EnumArrayAttribute))]
	public sealed class EnumArrayDrawer : DrawerBase
	{
		protected override void OnGUIInternal(Rect position, SerializedProperty property, GUIContent label)
		{
			var local = (EnumArrayAttribute) attribute;
			string name;
			try
			{
				var arrayPosition = int.Parse(property.propertyPath.Split('[', ']')[1]);
				name = Enum.GetName(local.EnumType, arrayPosition);
				if (name is null or "ENUM_LENGTH") name = "UNKNOWN";
			}
			catch
			{
				name = "ERROR";
			}
			
			EditorGUI.PropertyField(position, property, new GUIContent(name));
		}
	}
}