using Interactables;
using Managers;
using UnityEngine;

namespace Callbacks
{
    public class NightEndActions : MonoBehaviour
    {
        [SerializeField] private Interactable[] m_callOnNightEnd;
        
        public void DoActions()
        {
            if (m_callOnNightEnd.Length > 0)
            {
                foreach (var interactable in m_callOnNightEnd) interactable.Interact();
            }
        }
    }
}