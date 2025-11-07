using System.Collections.Generic;
using UnityEngine;

namespace Physics
{
	[AddComponentMenu("Physics/Aggregate Collider")]
	public sealed class CollisionAggregate : MonoBehaviour
	{
		[SerializeField] private CollisionReceiver[] collisionReceivers;
		
		private IOnCollisionEnter[] m_onCollisionEnter;
		private IOnCollisionExit[] m_onCollisionExit;
		private IOnTriggerEnter[] m_onTriggerEnter;
		private IOnTriggerExit[] m_onTriggerExit;

		private void Start()
		{
			var onCollisionEnter = new List<IOnCollisionEnter>();
			var onCollisionExit = new List<IOnCollisionExit>();
			var onTriggerEnter = new List<IOnTriggerEnter>();
			var onTriggerExit = new List<IOnTriggerExit>();
			for (var i = 0; i < collisionReceivers.Length; i++)
			{
				var receiver = collisionReceivers[i];
				if (receiver is IOnCollisionEnter collisionEnter) onCollisionEnter.Add(collisionEnter);
				if (receiver is IOnCollisionExit collisionExit) onCollisionExit.Add(collisionExit);
				if (receiver is IOnTriggerEnter triggerEnter) onTriggerEnter.Add(triggerEnter);
				if (receiver is IOnTriggerExit triggerExit) onTriggerExit.Add(triggerExit);
			}
			
			if (onCollisionEnter.Count > 0) m_onCollisionEnter = onCollisionEnter.ToArray();
			if (onCollisionExit.Count > 0) m_onCollisionExit = onCollisionExit.ToArray();
			if (onTriggerEnter.Count > 0) m_onTriggerEnter = onTriggerEnter.ToArray();
			if (onTriggerExit.Count > 0) m_onTriggerExit = onTriggerExit.ToArray();
		}

		private void OnCollisionEnter(Collision other)
		{
			if (m_onCollisionEnter == null) return;
			for (var i = 0; i < m_onCollisionEnter.Length; i++) 
				m_onCollisionEnter[i].OnCollisionEnterImplementation(other);
		}

		private void OnCollisionExit(Collision other)
		{
			if (m_onCollisionExit == null) return;
			for (var i = 0; i < m_onCollisionExit.Length; i++) 
				m_onCollisionExit[i].OnCollisionExitImplementation(other);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (m_onTriggerEnter == null) return;
			for (var i = 0; i < m_onTriggerEnter.Length; i++) 
				m_onTriggerEnter[i].OnTriggerEnterImplementation(other);
		}

		private void OnTriggerExit(Collider other)
		{
			if (m_onTriggerExit == null) return;
			for (var i = 0; i < m_onTriggerExit.Length; i++) 
				m_onTriggerExit[i].OnTriggerExitImplementation(other);
		}
	}
}