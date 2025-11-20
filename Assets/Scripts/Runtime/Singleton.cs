using System.Threading;
using UnityEngine;

namespace Runtime
{
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T m_instance;

		public static T Instance => m_instance;
	
		protected abstract bool Override { get; }

		//Now fully atomic
		protected virtual void Awake()
		{
			if (Override) Interlocked.Exchange(ref m_instance, this as T);
			else Interlocked.CompareExchange(ref m_instance, this as T, null);
		}

		//Now fully atomic
		protected virtual void OnDestroy() => Interlocked.CompareExchange(ref m_instance, null, this as T);
	}
}