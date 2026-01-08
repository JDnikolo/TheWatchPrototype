using UI.Elements;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(ComboBox), true)]
	[CanEditMultipleObjects]
	public sealed class ComboboxEditor : ParentEditor
	{
		protected override void DisplayHidden()
		{
			base.DisplayHidden();
			if (EditorApplication.isPlaying)
			{
				var local = (ComboBox) target;
				local.CurrentData.DisplayInEditor("Current Data");
				local.DataProvider.Display("Data Provider");
				EditorGUILayout.Toggle("Opened", local.EditorOpened);
			}
		}
	}
}