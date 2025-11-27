using System;
using Localization.Text;
using Runtime.Automation;
using UnityEditor;
using Utilities;

namespace UI.ComboBox
{
	public struct ComboData : IEquatable<ComboData>
#if UNITY_EDITOR
		, IEditorDisplayable
#endif
	{
		public TextObject Label;
		public object UserData;

		public ComboData(TextObject label, object userData)
		{
			Label = label;
			UserData = userData;
		}

		public bool Equals(ComboData other) => Equals(UserData, other.UserData);

		public override bool Equals(object obj) => obj is ComboData other && Equals(other);

		public override int GetHashCode() => (UserData != null ? UserData.GetHashCode() : 0);

		public override string ToString() => $"\"{Label}\" {UserData}";

		public static bool operator ==(ComboData left, ComboData right) => left.Equals(right);

		public static bool operator !=(ComboData left, ComboData right) => !left.Equals(right);
#if UNITY_EDITOR
		public void DisplayInEditor()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.ObjectField(Label, typeof(TextObject), false);
			UserData.Display(null);
			EditorGUILayout.EndHorizontal();
		}

		public void DisplayInEditor(string name) => name.DisplayIndented(DisplayInEditor);
#endif
	}
}