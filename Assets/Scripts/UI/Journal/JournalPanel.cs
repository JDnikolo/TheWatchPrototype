using System;
using Localization.Speaker;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using UnityEngine.InputSystem;
using Utilities;

namespace UI.Journal
{
    public class JournalPanel : MonoBehaviour, IFrameUpdatable
    {
        public byte UpdateOrder { get; } = byte.MinValue;
        
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private GameObject panelBase;

        [SerializeField] private GameObject journalEntryPrefab;

        [SerializeField] private string toggleJournalActionName = "Journal";

        [SerializeField] private int maxJournalEntries = 100;
        
        private int m_journalEntryCount = 0;
        
        // //
        private Updatable m_updatable = new Updatable();
        
        // //
        private InputAction m_openJournalAction;
        private InputAction JournalAction
        {
            get
            {
                return m_openJournalAction ??= InputManager.Instance.GetPlayerAction(toggleJournalActionName);
            }
        }
        
        private InputAction m_openJournalActionUI;
        private InputAction JournalActionUI
        {
            get
            {
                return m_openJournalActionUI ??= InputManager.Instance.GetUIAction(toggleJournalActionName);
            }
        }

        private bool m_requiresUpdate = false;
        
        public void AddNewJournalEntry(SpeakerObject speakerObject)
        {
            var newEntry = Instantiate(journalEntryPrefab, scrollRect.content, false);
            var je = newEntry.GetComponent<JournalEntry>();
            var profile = speakerObject.Profile;
            
            je.SetQuoteText(speakerObject.Text);
            je.SetNameText(profile? profile.CharacterName : "");
            

            if (++m_journalEntryCount >= maxJournalEntries) RemoveOldestEntry();
            
            m_requiresUpdate = true;
        }

        private void RemoveOldestEntry()
        {
            Destroy(scrollRect.content.transform.GetChild(0).gameObject);
        }

        public void ClearPanel()
        {
            for (int i = 0; i < scrollRect.content.transform.childCount; i++)
            {
                Destroy(scrollRect.content.transform.GetChild(i).gameObject);
            }
        }

        public void ShowJournalPanel() => panelBase.SetActive(true);
        
        public void HideJournalPanel() => panelBase.SetActive(false);
        
        public bool IsPanelVisible => panelBase.activeSelf;

        public void SetLoggingEnabled(bool value) => m_updatable.SetUpdating(value, this);
        
        public void OnFrameUpdate()
        {
            if (m_requiresUpdate)
            {
                scrollRect.verticalScrollbar.value = 1.0f;
                scrollRect.verticalScrollbar.value = 0.0f;
                m_requiresUpdate = false;
            }
            if (JournalActionUI.WasPressedThisFrame() || JournalAction.WasPressedThisFrame())
            {
                if (panelBase.activeSelf)
                {
                    UIManager.Instance.CloseJournalPanel();
                }
                else
                {
                    UIManager.Instance.OpenJournalPanel();
                }
            }
        }
        
        private void Awake()
        {
            GameManager.Instance.AddFrameUpdateSafe(this);
            m_updatable.SetUpdating(true, this);
            panelBase.SetActive(false);
        }
        
        private void OnDestroy() => GameManager.Instance.RemoveFrameUpdateSafe(this);
    }
}