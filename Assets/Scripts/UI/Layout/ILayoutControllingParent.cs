namespace UI.Layout
{
	public interface ILayoutControllingParent : ILayoutParent
	{
		ILayoutElement OnSelectNewElement(ILayoutElement oldElement, Direction input);
		
		void OnSelectingNewHierarchy(ILayoutElement newElement, ILayoutElement oldElement);
		
#if UNITY_EDITOR
		LayoutBlockedDirections BlockedDirections { get; }
#endif
	}
}