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
			public ILayoutInput CurrentInput;
#if UNITY_EDITOR
			void IEditorDisplayable.DisplayInEditor()
			{
				ParentHierarchy.DisplayObjectCollection("Parent Hierarchy");
				CurrentElement.DisplayObject("Current Element");
				CurrentInput.DisplayObject("Current Input");
			}
#endif
		}
	}
}