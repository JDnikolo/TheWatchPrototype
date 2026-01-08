using System.Collections.Generic;
using Attributes;
using Audio;
using Callbacks.Backing;
using Callbacks.Pausing;
using Input;
using Managers.Persistent;
using Runtime;
using Runtime.FixedUpdate;
using Runtime.FrameUpdate;
using Runtime.LateUpdate;
using UI.Layout.Elements;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Managers
{
	[AddComponentMenu("Managers/Pause Manager")]
	public sealed partial class PauseManager : Singleton<PauseManager>, IBackHook
	{
		[CanBeNullInPrefab, SerializeField] private LayoutElementBase pauseMenu;
		[SerializeField] private InputActionReference inputReference;
		[SerializeField] private AudioSnapshot pauseSnapshot;
		
		private HashSet<IPauseCallback> m_pauseCallbacks = new();
		private State m_pauseState;
		private bool m_paused;
		
		protected override bool Override => true;

		public bool CanPause { get; set; }

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
		
		public void OnBackPressed(InputState inputState)
		{
			if (inputState == InputState.Pressed)
			{
				var state = pauseMenu.gameObject.activeInHierarchy;
				if (state) m_pauseState.SaveStates(this);
				else if (CanPause)
				{
					var audioManager = AudioManager.Instance;
					audioManager.PreparePause(false, 0.2f);
					m_pauseState.LoadStates(this);
					audioManager.SetSnapshot(pauseSnapshot, false, 0.2f);
					var gameManager = GameManager.Instance;
					gameManager.FrameUpdateInvoke = FrameUpdatePosition.LayoutManager;
					gameManager.LateUpdateInvoke = LateUpdatePosition.None;
					gameManager.FixedUpdateInvoke = FixedUpdatePosition.None;
					InputManager.Instance.ForceUIInput();
					LayoutManager.Instance.ForceSelect(pauseMenu);
				}
			}
		}

		private void OnEnable() => InputManager.Instance?.BackSpecial.AddHook(this);

		private void OnDisable() => InputManager.Instance?.BackSpecial.RemoveHook(this);

#if UNITY_EDITOR
		public State PauseState => m_pauseState;
#endif
	}
}