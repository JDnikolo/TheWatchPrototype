using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Localization.Speaker;
using Localization.Text;
using UI.Journal;
using UnityEngine;

namespace Managers
{
    public class JournalManager : Singleton<JournalManager>
    {
        private HashSet<SpeakerObject> m_viewedTexts = new HashSet<SpeakerObject>();
        
        private Dictionary<int, SpeakerObject> m_mapToSpeaker = new Dictionary<int, SpeakerObject>();
        
        private List<int> m_textOrder = new List<int>();
        
        [SerializeField] private JournalPanel m_journalPanel;

        protected override bool Override { get; } = true;
        
        public void AddText(SpeakerObject speaker)
        {
            if (m_viewedTexts.Add(speaker))
            {
                m_mapToSpeaker.Add(speaker.GetHashCode(), speaker);
            }

            m_textOrder.Add(speaker.GetHashCode());
            
            m_journalPanel.AddNewJournalEntry(speaker);
        }

        public List<SpeakerObject> GetTexts()
        {
            return m_textOrder.Select(i => m_mapToSpeaker[i]).ToList();
        }

        public void ClearJournal()
        {
            m_textOrder.Clear();
            m_mapToSpeaker.Clear();
            m_viewedTexts.Clear();
            m_journalPanel.ClearPanel();
        }
        
    }
}