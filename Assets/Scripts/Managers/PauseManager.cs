using Managers.Persistent;
using Runtime;
using Runtime.FixedUpdate;
using Runtime.FrameUpdate;
using Runtime.LateUpdate;
using UI.Layout;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Managers
{
	[AddComponentMenu("Managers/Pause Manager")]
	public sealed partial class PauseManager : Singleton<PauseManager>, IFrameUpdatable
	{
		[SerializeField] private LayoutElement pauseMenu;
		[SerializeField] private string pauseActionName = "Pause";
		
		private State m_pauseState;

		private InputAction m_pauseAction;
		private Updatable m_updatable;
		
		protected override bool Override => true;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.PauseManager;

		public bool CanPause
		{
			get => m_updatable.Updating;
			set => m_updatable.SetUpdating(value, this);
		}
		
		public void OnFrameUpdate()
		{
			m_pauseAction ??= InputManager.Instance.PersistentGameMap.GetAction(pauseActionName);
			if (CanPause && m_pauseAction.WasPressedThisFrame())
			{
				var pauseObject = pauseMenu.gameObject;
				if (!pauseObject.activeInHierarchy)
				{
					m_pauseState.LoadStates();
					var gameManager = GameManager.Instance;
					gameManager.FrameUpdateInvoke = FrameUpdatePosition.PauseManager;
					gameManager.LateUpdateInvoke = LateUpdatePosition.None;
					gameManager.FixedUpdateInvoke = FixedUpdatePosition.None;
					InputManager.Instance.ForceUIInput();
					LayoutManager.Instance.ForceSelect(pauseMenu);
				}
				else m_pauseState.SaveStates();
			}
		}
#if UNITY_EDITOR
		public State PauseState => m_pauseState;
#endif
	}
}