using Highlighting;
using LookupTables;
using Managers.Persistent;
using Runtime;
using Runtime.FixedUpdate;
using Runtime.FrameUpdate;
using UnityEngine;
using Utilities;

namespace Managers
{
	[AddComponentMenu("Managers/Highlight Manager")]
	public sealed class HighlightManager : Singleton<HighlightManager>, IFrameUpdatable, IFixedUpdatable
	{
		[SerializeField] private LayerMask highlightMask;

		private IManagedHighlightable m_raycastTarget;
		private IManagedHighlightable m_previousTarget;

		protected override bool Override => true;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.HighlightManager;

		public FixedUpdatePosition FixedUpdateOrder => FixedUpdatePosition.HighlightManager;

		public void OnFrameUpdate()
		{
			if (m_previousTarget == m_raycastTarget) return;
			if (m_previousTarget != null) m_previousTarget.Highlight(false);
			m_previousTarget = m_raycastTarget;
			if (m_previousTarget != null) m_previousTarget.Highlight(true);
		}

		public void OnFixedUpdate()
		{
			var cameraTransform = PlayerManager.Instance.PlayerCamera.transform;
			if (UnityEngine.Physics.Raycast(cameraTransform.position, cameraTransform.forward,
					out var hit, 3f, highlightMask.value))
			{
				var rigidBodyTable = RigidBodyTable.Instance;
				if (rigidBodyTable && rigidBodyTable.TryGetValue(hit.rigidbody, out var extender))
				{
					var highlightable = extender.Highlightable;
					if (highlightable != null)
						m_raycastTarget = CheckDistances(hit.rigidbody, highlightable, cameraTransform.position)
							? highlightable
							: null;
				}
				else m_raycastTarget = null;
			}
			else m_raycastTarget = null;
		}

		private static bool CheckDistances(Rigidbody rigidbody, IManagedHighlightable highlightable, Vector3 position)
		{
			var lengthSqr = Vector3.SqrMagnitude(rigidbody.position - position);
			return lengthSqr >= highlightable.MinHighlightDistance.Squared() &&
					lengthSqr <= highlightable.MaxHighlightDistance.Squared();
		}

		protected override void Awake()
		{
			base.Awake();
			var gameManager = GameManager.Instance;
			if (gameManager)
			{
				gameManager.AddFrameUpdate(this);
				gameManager.AddFixedUpdate(this);
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			m_raycastTarget = null;
			m_previousTarget = null;
			var gameManager = GameManager.Instance;
			if (gameManager)
			{
				gameManager.RemoveFrameUpdate(this);
				gameManager.RemoveFixedUpdate(this);
			}
		}
#if UNITY_EDITOR
		public IManagedHighlightable RaycastTarget => m_raycastTarget;
#endif
	}
}