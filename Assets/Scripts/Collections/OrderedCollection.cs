using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    public abstract class OrderedCollection<T> : ICollection<T>
    {
        protected SortedDictionary<byte, HashSet<T>> m_collections = new();
        private HashSet<T> m_toAdd = new();
        private HashSet<T> m_toRemove = new();
        private HashSet<T> m_contained = new();

        public int Count => m_collections.Count;

        bool ICollection<T>.IsReadOnly => false;
        
        public struct Enumerator : IEnumerator<T>
        {
            private OrderedCollection<T> m_collection;
            private T m_current;
            private bool m_started;

            private SortedDictionary<byte, HashSet<T>>.Enumerator m_outerEnumerator;
            private HashSet<T>.Enumerator m_innerEnumerator;
            
            public T Current => m_current;

            object IEnumerator.Current => Current;
            
            public Enumerator(OrderedCollection<T> collection)
            {
                m_collection = collection;
                m_current = default;
                m_started = false;
            }

            public bool MoveNext()
            {
                if (!m_started)
                {
                    //Set up the first stage of enumeration
                    m_started = true;
                    m_outerEnumerator = m_collection.m_collections.GetEnumerator();
                    if (!m_outerEnumerator.MoveNext()) return false;
                    m_innerEnumerator = m_outerEnumerator.Current.Value.GetEnumerator();
                }

                //Check for the inner enumerators members
                Evaluate:
                if (m_innerEnumerator.MoveNext())
                {
                    m_current = m_innerEnumerator.Current;
                    return true;
                }
                
                //Since this enumerator is finished, we check if the outer can move forward
                m_innerEnumerator.Dispose();
                if (!m_outerEnumerator.MoveNext()) return false;
                //Then we re-assign the inner and evaluate again
                m_innerEnumerator = m_outerEnumerator.Current.Value.GetEnumerator();
                goto Evaluate;
            }

            void IEnumerator.Reset() => throw new NotImplementedException();
            
            public void Dispose()
            {
                m_current = default;
                m_outerEnumerator.Dispose();
                m_innerEnumerator.Dispose();
                m_started = false;
            }
        }
        
        public void Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (m_contained.Contains(item)) return;
            m_toRemove.Remove(item);
            m_toAdd.Add(item);
        }

        public void Clear()
        {
            foreach (var collection in m_collections.Values) collection.Clear();
            m_collections.Clear();
            m_toAdd.Clear();
            m_toRemove.Clear();
            m_contained.Clear();
        }
        
        public bool Contains(T item) => m_contained.Contains(item);
        
        public void CopyTo(T[] array, int arrayIndex) => throw new NotImplementedException();

        public bool Remove(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (!m_contained.Contains(item)) return false;
            m_toAdd.Remove(item);
            return m_toRemove.Add(item);
        }

        public void Update(byte position)
        {
            m_toRemove.RemoveWhere(RemoveFirst);
            m_toAdd.RemoveWhere(AddLast);
            foreach (var pair in m_collections)
            {
                if (pair.Key > position) break;
                foreach (var item in pair.Value) Update(item);
            }
        }

        private bool AddLast(T item)
        {
            var priority = GetPriority(item);
            if (!m_collections.TryGetValue(priority, out var collection))
            {
                collection = new HashSet<T>();
                m_collections.Add(priority, collection);
            }

            m_toRemove.Remove(item);
            m_contained.Add(item);
            collection.Add(item);
            return true;
        }
        
        private bool RemoveFirst(T item)
        {
            var priority = GetPriority(item);
            if (m_collections.TryGetValue(priority, out var collection))
            {
                collection.Remove(item);
                m_contained.Remove(item);
                if (collection.Count == 0) m_collections.Remove(priority);
            }

            return true;
        }

        protected abstract byte GetPriority(T item);

        protected abstract void Update(T item);

        public Enumerator GetEnumerator() => new(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
#if UNITY_EDITOR
        public SortedDictionary<byte, HashSet<T>> Collections => m_collections;
#endif
    }
}