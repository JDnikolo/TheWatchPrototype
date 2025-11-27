using UI.ComboBox;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(ComboPanel))]
	public class ComboPanelEditor : ListBaseEditor
	{
		protected override void DisplayHidden()
		{
			base.DisplayHidden();
			var local = (ComboPanel) target;
			if (EditorApplication.isPlaying)
			{
				local.Parent.Display("Parent");
				local.OnFinished.Display("Callback Target");
			}

			local.Elements.DisplayCollection("Elements");
		}
	}
}