using System;
using System.Collections.Generic;
using Collections;
using UnityEngine;

namespace Managers
{
	[AddComponentMenu("Managers/Game Manager")]
	public sealed class GameManager : Singleton<GameManager>
	{
		private readonly FrameUpdateCollection m_frameUpdateCollection = new();
		private SwapStack<Action> m_frameInvokeStack = new(new Stack<Action>(), new Stack<Action>());
		private bool m_requireFrameUpdate;
		
		private readonly FixedUpdateCollection m_fixedUpdateCollection = new();
		private SwapStack<Action> m_fixedInvokeStack = new(new Stack<Action>(), new Stack<Action>());
		private bool m_requireFixedUpdate;

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
		
		public bool RequiresFrameUpdate
		{
			//get => m_requireFrameUpdate;
			set => m_requireFrameUpdate = value;
		}
		
		public bool RequiredFixedUpdate
		{
			//get => m_requireFixedUpdate;
			set => m_requireFixedUpdate = value;
		}
		
		protected override bool Override => false;
		
		internal void BeginLoad()
		{
			//Clear out the game state to zero
			m_gameState = GameState.Loading;
			m_frameUpdateCollection.Clear();
			m_frameInvokeStack.Clear();
			m_requireFrameUpdate = false;
			m_fixedUpdateCollection.Clear();
			m_fixedInvokeStack.Clear();
			m_requireFixedUpdate = false;
			m_localManager = LocalManager.Instance;
		}

		internal void EndLoad()
		{
			//We set to before play we can swap the cameras
			m_gameState = GameState.BeforePlay;
		}

		//Do not call from within the same invocation frame
		public void InvokeOnNextFrameUpdate(Action action)
		{
			if (action != null) m_frameInvokeStack.Push(action);
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
					var highlightManager = HighlightManager.Instance;
					AddFrameUpdate(highlightManager);
					AddFixedUpdate(highlightManager);
					SettingsManager.Instance.Load();
					m_gameState = GameState.BeforePlay;
					break;
				case GameState.BeforePlay:
					//This will delay it until the local manager has changed instance
					if (m_localManager && LocalManager.Instance == m_localManager) return;
					m_localManager = null;
					CameraManager.Instance.SetCamera(PlayerManager.Instance.PlayerCamera);
					m_gameState = GameState.Play;
					break;
				case GameState.Play:
					var stack = m_frameInvokeStack.Swap();
					while (stack.TryPop(out var action)) action.Invoke();
					if (m_requireFrameUpdate) m_frameUpdateCollection.Update();
					break;
				case GameState.Loading:
					SceneManager.Instance.ProcessScenes();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private void FixedUpdate()
		{
			var stack = m_fixedInvokeStack.Swap();
			while (stack.TryPop(out var action)) action.Invoke();
			if (m_gameState == GameState.Play || m_requireFixedUpdate) m_fixedUpdateCollection.Update();
		}
	}
}