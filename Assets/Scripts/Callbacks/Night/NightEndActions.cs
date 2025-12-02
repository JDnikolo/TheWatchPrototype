using Interactables;
using UnityEngine;
using UnityEngine.Serialization;

namespace Callbacks.Night
{
    public class NightEndActions : MonoBehaviour
    {
        [FormerlySerializedAs("m_callOnNightEnd")] [SerializeField] private Interactable[] callOnNightEnd;
        
        public void DoActions()
        {
            if (callOnNightEnd.Length > 0)
            {
                foreach (var interactable in callOnNightEnd) interactable.Interact();
            }
        }
    }
}