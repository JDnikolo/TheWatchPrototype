using Runtime;
using Runtime.FixedUpdate;
using UnityEngine;

namespace Managers.Persistent
{
	[AddComponentMenu("Managers/Persistent/Physics Manager")]
	public sealed class PhysicsManager : Singleton<PhysicsManager>, IFixedUpdatable
	{
		private Updatable m_updatable;
		
		private bool m_requirePhysics;
		private bool m_requirePhysics2D;
		
		protected override bool Override => false;

		public FixedUpdatePosition FixedUpdateOrder => FixedUpdatePosition.PhysicsManager;

		public bool RequirePhysics
		{
			get => m_requirePhysics;
			set
			{
				if (m_requirePhysics == value) return;
				m_requirePhysics = value;
				CheckUpdate();
			}
		}
		
		public bool RequirePhysics2D
		{
			get => m_requirePhysics2D;
			set
			{
				if (m_requirePhysics2D == value) return;
				m_requirePhysics2D = value;
				CheckUpdate();
			}
		}
		
		public void Stop() => RequirePhysics = RequirePhysics2D = false;

		public void OnFixedUpdate()
		{
			var fixedDeltaTime = Time.fixedDeltaTime;
			if (m_requirePhysics) UnityEngine.Physics.Simulate(fixedDeltaTime);
			if (m_requirePhysics2D) Physics2D.Simulate(fixedDeltaTime);
		}

		private void CheckUpdate() => m_updatable.SetUpdating(m_requirePhysics || m_requirePhysics2D, null, this);
	}
}