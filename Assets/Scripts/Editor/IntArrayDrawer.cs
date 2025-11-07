using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(IntArrayAttribute))]
	public sealed class IntArrayDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{
			var intAttribute = (IntArrayAttribute) attribute;
			string name;
			try
			{
				var arrayPosition = int.Parse(property.propertyPath.Split('[', ']')[1]) + 1;
				if (string.IsNullOrEmpty(intAttribute.Format)) name = arrayPosition.ToString();
				else name = string.Format(intAttribute.Format, arrayPosition);
			}
			catch
			{
				name = "ERROR";
			}
			
			EditorGUI.PropertyField(rect, property, new GUIContent(name));
		}
	}
}