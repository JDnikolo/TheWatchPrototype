using System;
using Localization;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomEditor(typeof(Language))]
	public class LanguageEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (target is not Language local) return;
			if (GUILayout.Button("Check localization"))
			{
				var failed = false;
				var textObjects = local.Texts;
				var textValues = Enum.GetValues(typeof(TextsEnum));
				int i;
				if (textObjects.Length > textValues.Length - 1)
					for (i = textValues.Length - 1; i < textObjects.Length; i++)
						Debug.Log($"[{i}]{textObjects[i].name} does not exist as a part of {nameof(TextsEnum)}!");
				else
				{
					foreach (TextsEnum text in textValues)
					{
						if (text == TextsEnum.ENUM_LENGTH) break;
						i = (int) text;
						var textName = textObjects[i].name;
						var textString = text.ToString();
						if (!textName.StartsWith(textString))
						{
							failed = true;
							Debug.Log($"[{i}]{textName} should be prefixed with {textString}, check order!");
						}
					}

					if (!failed) Debug.Log("All text is in the correct order.");
				}
			}
		}
	}
}