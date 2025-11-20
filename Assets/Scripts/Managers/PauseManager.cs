using Managers.Persistent;
using Runtime;
using Runtime.FrameUpdate;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
	[AddComponentMenu("Managers/Pause Manager")]
	public sealed class PauseManager : Singleton<PauseManager>, IFrameUpdatable
	{
		[SerializeField] private string pauseActionName = "Pause";

		private InputAction m_pauseAction;
		
		protected override bool Override => true;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.PauseManager;

		private FrameUpdatePosition m_previousState;
		
		public void OnFrameUpdate()
		{
			m_pauseAction ??= InputManager.Instance.GetPersistentGameAction(pauseActionName);
		}

		protected override void Awake()
		{
			base.Awake();
			var gameManager = GameManager.Instance;
			if (gameManager) gameManager.AddFrameUpdate(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			var gameManager = GameManager.Instance;
			if (gameManager) gameManager.RemoveFrameUpdate(this);
		}
	}
}