using System.Collections;
using System.Collections.Generic;

namespace Collections
{
	public class LinkedGrid<T> : ICollection<T>, IReadOnlyCollection<T>
	{
		public struct Enumerator : IEnumerator<T>
		{
			private LinkedGrid<T> m_grid;
			private T m_current;

			public T Current => m_current;

			object IEnumerator.Current => Current;

			public Enumerator(LinkedGrid<T> grid)
			{
				m_grid = grid;
				m_current = default;
			}

			public bool MoveNext()
			{
				throw new System.NotImplementedException();
			}

			public void Reset()
			{
				throw new System.NotImplementedException();
			}
			
			public void Dispose()
			{
				// TODO release managed resources here
			}
		}
		
		public int Count => throw new System.NotImplementedException();

		public bool IsReadOnly => throw new System.NotImplementedException();
		
		public void Add(T item)
		{
			throw new System.NotImplementedException();
		}

		public void Clear()
		{
			throw new System.NotImplementedException();
		}

		public bool Contains(T item)
		{
			throw new System.NotImplementedException();
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			throw new System.NotImplementedException();
		}

		public bool Remove(T item)
		{
			throw new System.NotImplementedException();
		}

		public Enumerator GetEnumerator() => new Enumerator(this);
		
		IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
		
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}