using System.Collections.Generic;
using System.Linq;
using Localization.Speaker;
using Runtime;
using UI.Journal;
using UnityEngine;

namespace Managers
{
    [AddComponentMenu("Managers/Journal Manager")]
    public sealed class JournalManager : Singleton<JournalManager>
    {
        [SerializeField] private JournalPanel m_journalPanel;
        
        private HashSet<SpeakerObject> m_viewedTexts = new();
        private Dictionary<int, SpeakerObject> m_mapToSpeaker = new();
        private List<int> m_textOrder = new();

        protected override bool Override => true;

        public void AddText(SpeakerObject speaker)
        {
            if (m_viewedTexts.Add(speaker)) m_mapToSpeaker.Add(speaker.GetHashCode(), speaker);
            m_textOrder.Add(speaker.GetHashCode());
            m_journalPanel.AddNewJournalEntry(speaker);
        }

        public List<SpeakerObject> GetTexts() => m_textOrder.Select(i => m_mapToSpeaker[i]).ToList();

        public void ClearJournal()
        {
            m_textOrder.Clear();
            m_mapToSpeaker.Clear();
            m_viewedTexts.Clear();
            m_journalPanel.ClearPanel();
        }
    }
}