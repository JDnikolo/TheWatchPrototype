using System.Collections.Generic;
using Attributes;
using Audio;
using Callbacks.Pausing;
using Managers.Persistent;
using Runtime;
using Runtime.FixedUpdate;
using Runtime.FrameUpdate;
using Runtime.LateUpdate;
using UI.Layout;
using UI.Layout.Elements;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Managers
{
	[AddComponentMenu("Managers/Pause Manager")]
	public sealed partial class PauseManager : Singleton<PauseManager>, IFrameUpdatable
	{
		[CanBeNullInPrefab, SerializeField] private LayoutElementBase pauseMenu;
		[SerializeField] private InputActionReference inputReference;
		[SerializeField] private AudioSnapshot pauseSnapshot;
		
		private HashSet<IPauseCallback> m_pauseCallbacks = new();
		private State m_pauseState;
		private bool m_paused;
		
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
			if (m_paused == paused) return;
			m_paused = paused;
			GameManager.Instance.InvokeOnNextLateUpdate(SwitchPause);
		}

		private void SwitchPause()
		{
			foreach (var pauseCallback in m_pauseCallbacks) pauseCallback.OnPauseChanged(m_paused);
		}

		public void AddPausedCallback(IPauseCallback pausedCallback) => m_pauseCallbacks.Add(pausedCallback);

		public void RemovePausedCallback(IPauseCallback pausedCallback) => m_pauseCallbacks.Remove(pausedCallback);
		
		public void OnFrameUpdate()
		{
			if (CanPause && inputReference.action.WasPressedThisFrame())
			{
				var pauseObject = pauseMenu.gameObject;
				var state = pauseObject.activeInHierarchy;
				if (!state)
				{
					var audioManager = AudioManager.Instance;
					audioManager.PreparePause(false, 0.2f);
					m_pauseState.LoadStates(this);
					audioManager.SetSnapshot(pauseSnapshot, false, 0.2f);
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