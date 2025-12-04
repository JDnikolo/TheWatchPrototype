using System;
using System.Collections.Generic;
using System.IO;
using Audio;
using Callbacks.Beforeplay;
using Collections;
using Runtime;
using Runtime.FixedUpdate;
using Runtime.FrameUpdate;
using Runtime.LateUpdate;
using UnityEngine;

namespace Managers.Persistent
{
	[AddComponentMenu("Managers/Persistent/Game Manager")]
	public sealed partial class GameManager : Singleton<GameManager>
	{
		[SerializeField] private AudioSnapshot mainSnapshot;
		
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

		protected override bool Override => false;
		
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

		public State PauseState
		{
			get => new()
			{
				FrameUpdatePosition = FrameUpdateInvoke,
				LateUpdatePosition = LateUpdateInvoke,
				FixedUpdatePosition = FixedUpdateInvoke,
			};
			set
			{
				FrameUpdateInvoke = value.FrameUpdatePosition;
				LateUpdateInvoke = value.LateUpdatePosition;
				FixedUpdateInvoke = value.FixedUpdatePosition;
			}
		}
		
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
		
		public void AddFrameUpdate(IFrameUpdatable updatable) => m_frameUpdateCollection.Add(updatable);

		public void RemoveFrameUpdate(IFrameUpdatable updatable) => m_frameUpdateCollection.Remove(updatable);

		public void AddLateUpdate(ILateUpdatable updatable) => m_lateUpdateCollection.Add(updatable);

		public void RemoveLateUpdate(ILateUpdatable updatable) => m_lateUpdateCollection.Remove(updatable);

		public void AddFixedUpdate(IFixedUpdatable updatable) => m_fixedUpdateCollection.Add(updatable);

		public void RemoveFixedUpdate(IFixedUpdatable updatable) => m_fixedUpdateCollection.Remove(updatable);

		private void Update()
		{
			try
			{
				var audioManager = AudioManager.Instance;
				if (audioManager.RequireUpdate) audioManager.OnFrameUpdate();
				switch (m_gameState)
				{
					case GameState.Preload:
						SettingsManager.Instance.Load();
						AudioManager.Instance.SetSnapshot(mainSnapshot, false, 0f, 0f);
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
			catch (Exception e)
			{
				CreateLog(e);
				Application.Quit();
			}
		}

		private void LateUpdate()
		{
			try
			{
				if (m_gameState != GameState.Play) return;
				var stack = m_lateInvokeStack.Swap();
				while (stack.TryPop(out var action)) action.Invoke();
				m_lateUpdateCollection.Update((byte) m_lateUpdateInvoke);	
			}
			catch (Exception e)
			{
				CreateLog(e);
				Application.Quit();
			}
		}

		private void FixedUpdate()
		{
			try
			{
				if (m_gameState != GameState.Play) return;
				var stack = m_fixedInvokeStack.Swap();
				while (stack.TryPop(out var action)) action.Invoke();
				m_fixedUpdateCollection.Update((byte) m_fixedUpdateInvoke);
			}
			catch (Exception e)
			{
				CreateLog(e);
				Application.Quit();
			}
		}

		private void CreateLog(Exception e) => File.WriteAllText($"{Application.persistentDataPath}\\{DateTime.Now.ToLongDateString()}.crashdump",e.ToString());

		protected override void OnDestroy()
		{
			base.OnDestroy();
			m_frameUpdateCollection.Clear();
			m_frameUpdateCollection = null;
			m_frameInvokeStack.Dispose();
			m_frameInvokeStack = default;
			m_lateUpdateCollection.Clear();
			m_lateUpdateCollection = null;
			m_lateInvokeStack.Dispose();
			m_lateInvokeStack = default;
			m_fixedUpdateCollection.Clear();
			m_fixedUpdateCollection = null;
			m_fixedInvokeStack.Dispose();
			m_fixedInvokeStack = default;
			m_beforePlay.Clear();
			m_beforePlay = null;
			m_localManager = null;
		}
#if UNITY_EDITOR
		public OrderedCollection<IFrameUpdatable> FrameUpdateCollection => m_frameUpdateCollection;
		public OrderedCollection<ILateUpdatable> LateUpdateCollection => m_lateUpdateCollection;
		public OrderedCollection<IFixedUpdatable> FixedUpdateCollection => m_fixedUpdateCollection;
#endif
	}
}