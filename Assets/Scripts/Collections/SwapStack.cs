using System.Collections.Generic;

namespace Collections
{
	public struct SwapStack<T>
	{
		private Stack<T> m_mainCollection;
		private Stack<T> m_swapCollection;

		public SwapStack(Stack<T> mainCollection, Stack<T> swapCollection)
		{
			m_mainCollection = mainCollection;
			m_swapCollection = swapCollection;
		}

		public void Push(T item) => m_swapCollection.Push(item);

		public Stack<T> Swap()
		{
			(m_mainCollection, m_swapCollection) = (m_swapCollection, m_mainCollection);
			return m_mainCollection;
		}
		
		public void Clear()
		{
			m_mainCollection.Clear();
			m_swapCollection.Clear();
		}
	}
}