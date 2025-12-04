using Highlighting;
using UnityEngine;

namespace LookupTables
{
    [AddComponentMenu("Lookup Tables/Rigid Body Table")]
    public sealed class RigidBodyTable : LookupTable<RigidBodyTable, Rigidbody, RigidBodyExtender>
    {
        public void Add(Rigidbody key, IManagedHighlightable value)
        {
            m_lookupTable.TryGetValue(key, out var extender);
            extender.Highlightable = value;
            m_lookupTable[key] = extender;
        }
    }
}