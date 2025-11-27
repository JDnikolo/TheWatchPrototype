using Managers.Persistent;
using Runtime.Automation;
using Utilities;

namespace Managers
{
	public sealed partial class PauseManager
	{
		public struct State
#if UNITY_EDITOR
			: IEditorDisplayable
#endif
		{
			private GameManager.State m_gameManagerState;
			private InputManager.State m_inputManagerState;
			private LayoutManager.State m_layoutManagerState;
			private JournalManager.State m_journalManagerState;
			
			public void LoadStates()
			{
				m_gameManagerState = GameManager.Instance.PauseState;
				m_inputManagerState = InputManager.Instance.PauseState;
				m_layoutManagerState = LayoutManager.Instance.PauseState;
				m_journalManagerState = JournalManager.Instance.PauseState;
			}

			public void SaveStates()
			{
				GameManager.Instance.PauseState = m_gameManagerState;
				InputManager.Instance.PauseState = m_inputManagerState;
				LayoutManager.Instance.PauseState = m_layoutManagerState;
				JournalManager.Instance.PauseState = m_journalManagerState;
			}
#if UNITY_EDITOR
			public void DisplayInEditor()
			{
				m_gameManagerState.DisplayInEditor("Game Manager State");
				m_inputManagerState.DisplayInEditor("Input Manager State");
				m_layoutManagerState.DisplayInEditor("Layout Manager State");
				m_journalManagerState.DisplayInEditor("Journal Manager State");
			}
			
			public void DisplayInEditor(string name) => name.DisplayIndented(DisplayInEditor);
#endif
		}
	}
}