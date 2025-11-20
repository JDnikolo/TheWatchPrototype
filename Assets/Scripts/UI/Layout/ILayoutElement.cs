namespace UI.Layout
{
	public interface ILayoutElement
	{
		ILayoutElement Parent { get; set; }

		void Select();
		
		void Deselect();
	}
}