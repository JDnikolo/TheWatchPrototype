using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(IntArray))]
	public sealed class IntArrayDrawer : DrawerBase
	{
		protected override void OnGUIInternal(Rect position, SerializedProperty property, GUIContent label)
		{
			var intAttribute = (IntArray) attribute;
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
			
			EditorGUI.PropertyField(position, property, new GUIContent(name));
		}
	}
}