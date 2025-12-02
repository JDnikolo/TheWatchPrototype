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
			private bool m_pauseState;
			private GameManager.State m_gameManagerState;
			private InputManager.State m_inputManagerState;
			private LayoutManager.State m_layoutManagerState;
			private JournalManager.State m_journalManagerState;
			
			public void LoadStates(PauseManager pauseManager)
			{
				m_pauseState = pauseManager.m_paused;
				m_gameManagerState = GameManager.Instance.PauseState;
				m_inputManagerState = InputManager.Instance.PauseState;
				m_layoutManagerState = LayoutManager.Instance.PauseState;
				m_journalManagerState = JournalManager.Instance.PauseState;
				pauseManager.SetPause(true);
			}

			public void SaveStates(PauseManager pauseManager)
			{
				GameManager.Instance.PauseState = m_gameManagerState;
				InputManager.Instance.PauseState = m_inputManagerState;
				LayoutManager.Instance.PauseState = m_layoutManagerState;
				JournalManager.Instance.PauseState = m_journalManagerState;
				pauseManager.SetPause(m_pauseState);
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