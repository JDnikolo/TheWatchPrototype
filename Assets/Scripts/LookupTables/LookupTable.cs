using System.Collections.Generic;
using Runtime;
using UnityEngine;

namespace LookupTables
{
    public abstract class LookupTable<T, TKey, TValue> : Singleton<T> where T : MonoBehaviour
    {
        protected override bool Override => true;
        
        protected readonly Dictionary<TKey, TValue> m_lookupTable = new();
        
        public int Count => m_lookupTable.Count;
        
        public void Add(TKey key, TValue value) => m_lookupTable.Add(key, value);
        
        public bool Contains(TKey key) => m_lookupTable.ContainsKey(key);

        public bool TryGetValue(TKey key, out TValue value) => m_lookupTable.TryGetValue(key, out value);
        
        public bool Remove(TKey key) => m_lookupTable.Remove(key);
#if UNITY_EDITOR
        public Dictionary<TKey, TValue> LookupTableEditor => m_lookupTable;
#endif
    }
}