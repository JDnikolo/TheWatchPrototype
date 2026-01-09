using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Interactables;
using UnityEngine;

public class NightActionScheduler : BaseBehaviour
{
    [SerializeField] private SerializedDictionary<int, Interactable> scheduledActions = new SerializedDictionary<int, Interactable>();
    
    private SortedList<int,Interactable> m_sortedSchedule = new SortedList<int,Interactable>();

    public void Start()
    {
        m_sortedSchedule = new SortedList<int, Interactable>(scheduledActions);
    }

    public void CheckForScheduledActions(float currentTime)
    {
        if (m_sortedSchedule.Count == 0) return;
        
        var item = m_sortedSchedule.First();
        while (m_sortedSchedule.Count != 0 && item.Key < currentTime)
        {
            item.Value.Interact();
            m_sortedSchedule.RemoveAt(0);
            if (m_sortedSchedule.Count > 0) item = m_sortedSchedule.First();
        }
    }
}
