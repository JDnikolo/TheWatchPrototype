using Runtime.Automation;
using UI.Layout;
using Utilities;

namespace Managers
{
	public sealed partial class LayoutManager
	{
		public struct State
#if UNITY_EDITOR
			: IEditorDisplayable
#endif
		{
			public ILayoutElement[] ParentHierarchy;
			public ILayoutElement CurrentElement;
#if UNITY_EDITOR
			public void DisplayInEditor()
			{
				ParentHierarchy.DisplayCollection("Parent Hierarchy");
				CurrentElement.Display("Current Element");
			}
			
			public void DisplayInEditor(string name) => name.DisplayIndented(DisplayInEditor);
#endif
		}
	}
}