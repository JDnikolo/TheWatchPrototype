#if UNITY_EDITOR
namespace Runtime.Automation
{
	public interface IEditorDisplayable
	{
		void DisplayInEditor();
		
		void DisplayInEditor(string name);
	}
}
#endif