using Localization.Speaker;
using UnityEngine;
using UnityEngine.UI;
using Runtime.FrameUpdate;
using UnityEngine.InputSystem;

namespace UI.Journal
{
    [AddComponentMenu("UI/Journal/Journal Panel")]
    public sealed class JournalPanel : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private JournalEntry journalEntryPrefab;
        [SerializeField] private int maxJournalEntries = 100;

        private int m_journalEntryCount;
        private InputAction m_openJournalAction;

        public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.GameUI;
        
        private bool m_requiresUpdate;

        public void AddNewJournalEntry(SpeakerObject speakerObject)
        {
            var newEntry = Instantiate(journalEntryPrefab, scrollRect.content, false);
            var profile = speakerObject.Profile;
            newEntry.SetQuoteText(speakerObject.Text);
            newEntry.SetNameText(profile ? profile.CharacterName : "");
            if (++m_journalEntryCount >= maxJournalEntries) RemoveOldestEntry();
            m_requiresUpdate = true;
        }

        private void RemoveOldestEntry()
        {
            Destroy(scrollRect.content.transform.GetChild(0).gameObject);
        }

        public void ClearPanel()
        {
            for (var i = 0; i < scrollRect.content.transform.childCount; i++) 
                Destroy(scrollRect.content.transform.GetChild(i).gameObject);
        }

        public void OnFrameUpdate()
        {
            if (m_requiresUpdate)
            {
                scrollRect.verticalScrollbar.value = 1.0f;
                scrollRect.verticalScrollbar.value = 0.0f;
                m_requiresUpdate = false;
            }
        }
    }
}