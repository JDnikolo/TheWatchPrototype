using System.Collections.Generic;
using Callbacks.Pausing;
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

		private HashSet<IPauseCallback> m_pauseCallbacks = new();
		private State m_pauseState;
		private bool m_lastPaused;
		
		private InputAction m_pauseAction;
		private Updatable m_updatable;
		
		protected override bool Override => true;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.PauseManager;

		public bool CanPause
		{
			get => m_updatable.Updating;
			set => m_updatable.SetUpdating(value, this);
		}

		private void SetPause(bool paused)
		{
			if (m_lastPaused == paused) return;
			m_lastPaused = paused;
			foreach (var pauseCallback in m_pauseCallbacks) pauseCallback.OnPauseChanged(paused);
		}
		
		public void AddPausedCallback(IPauseCallback pausedCallback) => m_pauseCallbacks.Add(pausedCallback);

		public void RemovePausedCallback(IPauseCallback pausedCallback) => m_pauseCallbacks.Remove(pausedCallback);
		
		public void OnFrameUpdate()
		{
			m_pauseAction ??= InputManager.Instance.PersistentGameMap.GetAction(pauseActionName);
			if (CanPause && m_pauseAction.WasPressedThisFrame())
			{
				var pauseObject = pauseMenu.gameObject;
				var state = pauseObject.activeInHierarchy;
				if (!state)
				{
					m_pauseState.LoadStates(this);
					var gameManager = GameManager.Instance;
					gameManager.FrameUpdateInvoke = FrameUpdatePosition.PauseManager;
					gameManager.LateUpdateInvoke = LateUpdatePosition.None;
					gameManager.FixedUpdateInvoke = FixedUpdatePosition.None;
					InputManager.Instance.ForceUIInput();
					LayoutManager.Instance.ForceSelect(pauseMenu);
				}
				else m_pauseState.SaveStates(this);
			}
		}
#if UNITY_EDITOR
		public State PauseState => m_pauseState;
#endif
	}
}