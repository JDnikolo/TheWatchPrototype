using System.Collections.Generic;
using Highlighting;
using UnityEngine;

namespace Managers
{
	[AddComponentMenu("Managers/Highlight Manager")]
	public sealed class HighlightManager : Singleton<HighlightManager>, IFrameUpdatable, IFixedUpdatable
	{
		[SerializeField] private LayerMask highlightMask;
		
		private readonly Dictionary<Rigidbody, IHighlightable> m_rigidbodies = new();
		private IHighlightable m_raycastInteractable;
		private IHighlightable m_previousInteractable;
		
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
			if (m_previousInteractable == m_raycastInteractable) return;
			if (m_previousInteractable != null) m_previousInteractable.Highlight(false);
			m_previousInteractable = m_raycastInteractable;
			if (m_previousInteractable != null) m_previousInteractable.Highlight(true);
		}
		
		public void OnFixedUpdate()
		{
			var cameraTransform = PlayerManager.Instance.Camera.transform;
			if (UnityEngine.Physics.Raycast(cameraTransform.position, cameraTransform.forward,
					out var hit, 3f, highlightMask.value) && m_rigidbodies.TryGetValue(
					hit.rigidbody, out var interactable)) m_raycastInteractable = interactable;
			else m_raycastInteractable = null;
		}

		public void AddHighlightable(Rigidbody rigidbody, IHighlightable highlightable) => m_rigidbodies.Add(rigidbody, highlightable);

		public void RemoveHighlightable(Rigidbody rigidbody) => m_rigidbodies.Remove(rigidbody);
	}
}