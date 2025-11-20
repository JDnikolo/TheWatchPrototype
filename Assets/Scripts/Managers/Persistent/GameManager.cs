using System;
using System.Collections.Generic;
using Collections;
using Runtime;
using Runtime.FixedUpdate;
using Runtime.FrameUpdate;
using Runtime.LateUpdate;
using UnityEngine;

namespace Managers.Persistent
{
	[AddComponentMenu("Managers/Persistent/Game Manager")]
	public sealed class GameManager : Singleton<GameManager>
	{
		private FrameUpdateCollection m_frameUpdateCollection = new();
		private SwapStack<Action> m_frameInvokeStack = new(new Stack<Action>(), new Stack<Action>());
		private FrameUpdatePosition m_frameUpdateInvoke;
		
		private LateUpdateCollection m_lateUpdateCollection = new();
		private SwapStack<Action> m_lateInvokeStack = new(new Stack<Action>(), new Stack<Action>());
		private LateUpdatePosition m_lateUpdateInvoke;
		
		private FixedUpdateCollection m_fixedUpdateCollection = new();
		private SwapStack<Action> m_fixedInvokeStack = new(new Stack<Action>(), new Stack<Action>());
		private FixedUpdatePosition m_fixedUpdateInvoke;
		
		private Stack<IBeforePlay> m_beforePlay = new();
		private LocalManager m_localManager;
		
		private enum GameState : byte
		{
			/// <summary>
			/// First startup
			/// </summary>
			Preload,
			
			/// <summary>
			/// One frame before play update.
			/// </summary>
			BeforePlay,
			
			/// <summary>
			/// Update during play.
			/// </summary>
			Play,
			
			/// <summary>
			/// Only update the scene manager.
			/// </summary>
			Loading,
		}

		private GameState m_gameState = GameState.Preload;

		public FrameUpdatePosition FrameUpdateInvoke
		{
			get => m_frameUpdateInvoke;
			set => m_frameUpdateInvoke = value;
		}

		public FixedUpdatePosition FixedUpdateInvoke
		{
			get => m_fixedUpdateInvoke;
			set => m_fixedUpdateInvoke = value;
		}

		public LateUpdatePosition LateUpdateInvoke
		{
			get => m_lateUpdateInvoke;
			set => m_lateUpdateInvoke = value;
		}

		protected override bool Override => false;
		
		internal void BeginLoad()
		{
			//Clear out the game state to zero
			m_gameState = GameState.Loading;
			m_frameUpdateCollection.Clear();
			m_frameInvokeStack.Clear();
			m_frameUpdateInvoke = FrameUpdatePosition.None;
			m_lateUpdateCollection.Clear();
			m_lateInvokeStack.Clear();
			m_lateUpdateInvoke = LateUpdatePosition.None;
			m_fixedUpdateCollection.Clear();
			m_fixedInvokeStack.Clear();
			m_fixedUpdateInvoke = FixedUpdatePosition.None;
			m_localManager = LocalManager.Instance;
		}

		internal void EndLoad()
		{
			//We set to before play we can swap the cameras
			m_gameState = GameState.BeforePlay;
		}
		
		public void RegisterBeforePlay(IBeforePlay beforePlay) => m_beforePlay.Push(beforePlay);

		//Do not call from within the same invocation frame
		public void InvokeOnNextFrameUpdate(Action action)
		{
			if (action != null) m_frameInvokeStack.Push(action);
		}

		//Do not call from within the same invocation frame
		public void InvokeOnNextLateUpdate(Action action)
		{
			if (action != null) m_lateInvokeStack.Push(action);
		}
		
		//Do not call from within the same invocation frame
		public void InvokeOnNextFixedUpdate(Action action)
		{
			if (action != null) m_fixedInvokeStack.Push(action);
		}
		
		//Do not call from within the same update frame
		public void AddFrameUpdate(IFrameUpdatable updatable) => m_frameUpdateCollection.Add(updatable);
		
		//Do not call from within the same update frame
		public void RemoveFrameUpdate(IFrameUpdatable updatable) => m_frameUpdateCollection.Remove(updatable);
		
		//Do not call from within the same update frame
		public void AddFixedUpdate(IFixedUpdatable updatable) => m_fixedUpdateCollection.Add(updatable);
		
		//Do not call from within the same update frame
		public void RemoveFixedUpdate(IFixedUpdatable updatable) => m_fixedUpdateCollection.Remove(updatable);
		
		private void Update()
		{
			switch (m_gameState)
			{
				case GameState.Preload:
					SettingsManager.Instance.Load();
					m_gameState = GameState.BeforePlay;
					break;
				case GameState.BeforePlay:
					//This will delay it until the local manager has changed instance
					if (m_localManager && LocalManager.Instance == m_localManager) return;
					m_localManager = null;
					CameraManager.Instance.SetCamera(PlayerManager.Instance.PlayerCamera);
					while (m_beforePlay.TryPop(out var beforePlay)) beforePlay.OnBeforePlay();
					m_gameState = GameState.Play;
					break;
				case GameState.Play:
					var stack = m_frameInvokeStack.Swap();
					while (stack.TryPop(out var action)) action.Invoke();
					m_frameUpdateCollection.Update((byte) m_frameUpdateInvoke);
					break;
				case GameState.Loading:
					SceneManager.Instance.ProcessScenes();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void LateUpdate()
		{
			if (m_gameState != GameState.Play) return;
			var stack = m_lateInvokeStack.Swap();
			while (stack.TryPop(out var action)) action.Invoke();
			m_lateUpdateCollection.Update((byte) m_lateUpdateInvoke);
		}

		private void FixedUpdate()
		{
			if (m_gameState != GameState.Play) return;
			var stack = m_fixedInvokeStack.Swap();
			while (stack.TryPop(out var action)) action.Invoke();
			m_fixedUpdateCollection.Update((byte) m_fixedUpdateInvoke);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			m_frameUpdateCollection.Clear();
			m_frameUpdateCollection = null;
			m_frameInvokeStack.Clear();
			m_frameInvokeStack = default;
			m_fixedUpdateCollection.Clear();
			m_fixedUpdateCollection = null;
			m_fixedInvokeStack.Clear();
			m_fixedInvokeStack = default;
			m_beforePlay.Clear();
			m_beforePlay = null;
			m_localManager = null;
		}
	}
}