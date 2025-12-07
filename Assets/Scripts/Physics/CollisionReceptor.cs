using System.Collections.Generic;
using Callbacks.Physics;
using UnityEngine;

namespace Physics
{
	[AddComponentMenu("Physics/Collision Receptor")]
	public sealed class CollisionReceptor : BaseBehaviour
	{
		private readonly HashSet<IOnCollisionEnter> m_onCollisionEnter = new();
		private readonly HashSet<IOnCollisionExit> m_onCollisionExit = new();
		private readonly HashSet<IOnTriggerEnter> m_onTriggerEnter = new();
		private readonly HashSet<IOnTriggerExit> m_onTriggerExit = new();

		public void AddReceiver(object obj)
		{
			if (obj is IOnCollisionEnter onCollisionEnter) m_onCollisionEnter.Add(onCollisionEnter);
			if (obj is IOnCollisionExit onCollisionExit) m_onCollisionExit.Add(onCollisionExit);
			if (obj is IOnTriggerEnter onTriggerEnter) m_onTriggerEnter.Add(onTriggerEnter);
			if (obj is IOnTriggerExit onTriggerExit) m_onTriggerExit.Add(onTriggerExit);
		}

		public void RemoveReceiver(object obj)
		{
			if (obj is IOnCollisionEnter onCollisionEnter) m_onCollisionEnter.Remove(onCollisionEnter);
			if (obj is IOnCollisionExit onCollisionExit) m_onCollisionExit.Remove(onCollisionExit);
			if (obj is IOnTriggerEnter onTriggerEnter) m_onTriggerEnter.Remove(onTriggerEnter);
			if (obj is IOnTriggerExit onTriggerExit) m_onTriggerExit.Remove(onTriggerExit);
		}
		
		private void OnCollisionEnter(Collision other)
		{
			foreach (var onCollisionEnter in m_onCollisionEnter) 
				onCollisionEnter.OnCollisionEnterImplementation(other);
		}

		private void OnCollisionExit(Collision other)
		{
			foreach (var onCollisionExit in m_onCollisionExit)
				onCollisionExit.OnCollisionExitImplementation(other);
		}

		private void OnTriggerEnter(Collider other)
		{
			foreach (var onTriggerEnter in m_onTriggerEnter)
				onTriggerEnter.OnTriggerEnterImplementation(other);
		}

		private void OnTriggerExit(Collider other)
		{
			foreach (var onTriggerExit in m_onTriggerExit)
				onTriggerExit.OnTriggerExitImplementation(other);
		}
#if UNITY_EDITOR
		public HashSet<IOnCollisionEnter> OnCollisionEnterEditor => m_onCollisionEnter;
		
		public HashSet<IOnCollisionExit> OnCollisionExitEditor => m_onCollisionExit;
		
		public HashSet<IOnTriggerEnter> OnTriggerEnterEditor => m_onTriggerEnter;
		
		public HashSet<IOnTriggerExit> OnTriggerExitEditor => m_onTriggerExit;
#endif
	}
}