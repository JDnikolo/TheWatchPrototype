using System.Collections.Generic;
using Highlighting;
using UnityEngine;
using Utilities;

namespace Managers
{
	[AddComponentMenu("Managers/Highlight Manager")]
	public sealed class HighlightManager : Singleton<HighlightManager>, IFrameUpdatable, IFixedUpdatable
	{
		[SerializeField] private LayerMask highlightMask;
		
		private readonly Dictionary<Rigidbody, IManagedHighlightable> m_rigidbodies = new();
		private IManagedHighlightable m_raycastTarget;
		private IManagedHighlightable m_previousTarget;
		
		public byte UpdateOrder => 0;

		private void Start()
		{
			var gameManager = GameManager.Instance;
			if (gameManager)
			{
				gameManager.AddFrameUpdate(this);
				gameManager.AddFixedUpdate(this);
			}
		}

		protected override void OnDestroy()
		{
			var gameManager = GameManager.Instance;
			if (gameManager)
			{
				gameManager.RemoveFrameUpdate(this);
				gameManager.RemoveFixedUpdate(this);
			}
			
			base.OnDestroy();
		}

		public void OnFrameUpdate()
		{
			if (m_previousTarget == m_raycastTarget) return;
			if (m_previousTarget != null) m_previousTarget.Highlight(false);
			m_previousTarget = m_raycastTarget;
			if (m_previousTarget != null) m_previousTarget.Highlight(true);
		}

		public void OnFixedUpdate()
		{
			var cameraTransform = PlayerManager.Instance.Camera.transform;
			if (UnityEngine.Physics.Raycast(cameraTransform.position, cameraTransform.forward,
					out var hit, 3f, highlightMask.value) && m_rigidbodies.TryGetValue(
					hit.rigidbody, out var interactable) && CheckDistances(hit.rigidbody, 
					interactable, cameraTransform.position)) m_raycastTarget = interactable;
			else m_raycastTarget = null;
		}

		private static bool CheckDistances(Rigidbody rigidbody, IManagedHighlightable highlightable, Vector3 position)
		{
			var lengthSqr = Vector3.SqrMagnitude(rigidbody.position - position);
			return lengthSqr >= highlightable.MinHighlightDistance.Squared() &&
					lengthSqr <= highlightable.MaxHighlightDistance.Squared();
		}

		public void AddHighlightable(Rigidbody rigidbody, IManagedHighlightable highlightable) =>
			m_rigidbodies.Add(rigidbody, highlightable);

		public void RemoveHighlightable(Rigidbody rigidbody) => m_rigidbodies.Remove(rigidbody);
	}
}