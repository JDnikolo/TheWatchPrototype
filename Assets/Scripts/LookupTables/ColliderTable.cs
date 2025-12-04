using Interactables.Triggers;
using UnityEngine;

namespace LookupTables
{
    public sealed class ColliderTable : LookupTable<ColliderTable, Collider, ColliderExtender>
    {
        public void Add(Collider key, InteractableShoutTrigger value)
        {
            m_lookupTable.TryGetValue(key, out var extender);
            extender.ShoutTrigger = value;
            m_lookupTable[key] = extender;
        }
    }
}