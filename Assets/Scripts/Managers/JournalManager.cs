using Localization.Speaker;
using Managers.Persistent;
using Runtime;
using Runtime.FixedUpdate;
using Runtime.FrameUpdate;
using Runtime.LateUpdate;
using UI.Journal;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Managers
{
    [AddComponentMenu("Managers/Journal Manager")]
    public sealed partial class JournalManager : Singleton<JournalManager>, IFrameUpdatable
    {
        [SerializeField] private JournalPanel journalPanel;
        [SerializeField] private string journalActionName = "Journal";
        
        private PauseManager.State m_journalState;
        private InputAction m_journalAction;
        private Updatable m_updatable;
        
        protected override bool Override => true;

        public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.JournalManager;

        public bool CanOpenJournal
        {
            get => m_updatable.Updating;
            set => m_updatable.SetUpdating(value, this);
        }

        public State PauseState
        {
            get => new() {CanOpenJournal = CanOpenJournal};
            set => CanOpenJournal = value.CanOpenJournal;
        }
        
        public void OnFrameUpdate()
        {
            m_journalAction ??= InputManager.Instance.PersistentGameMap.GetAction(journalActionName);
            if (CanOpenJournal && m_journalAction.WasPressedThisFrame())
            {
                var journalObject = journalPanel.gameObject;
                if (!journalObject.activeInHierarchy)
                {
                    m_journalState.LoadStates(PauseManager.Instance);
                    var gameManager = GameManager.Instance;
                    gameManager.FrameUpdateInvoke = FrameUpdatePosition.JournalManager;
                    gameManager.LateUpdateInvoke = LateUpdatePosition.None;
                    gameManager.FixedUpdateInvoke = FixedUpdatePosition.None;
                    InputManager.Instance.ForceUIInput();
                    journalObject.SetActive(true);
                }
                else
                {
                    m_journalState.SaveStates(PauseManager.Instance);
                    journalObject.SetActive(false);
                }
            }
        }
        
        public void AddText(SpeakerObject speaker) => journalPanel.AddNewJournalEntry(speaker);

        public void ClearJournal() => journalPanel.ClearPanel();
    }
}