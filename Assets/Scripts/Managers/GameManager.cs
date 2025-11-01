using System;
using System.Collections.Generic;
using Collections;
using UnityEngine;
using Utilities;

namespace Managers
{
	[AddComponentMenu("Managers/Game Manager")]
	public sealed class GameManager : Singleton<GameManager>
	{
		private readonly FrameUpdateCollection m_frameUpdateCollection = new();
		private readonly Stack<Action> m_frameInvokeStack = new();
		
		private readonly FixedUpdateCollection m_fixedUpdateCollection = new();
		private readonly Stack<Action> m_fixedInvokeStack = new();
		
		private enum GameState : byte
		{
			Idle,
			Preload,
			Play,
		}

		private GameState m_gameState = GameState.Preload;
		
		internal void Stop() => m_gameState = GameState.Idle;

		internal void Restart() => m_gameState = GameState.Play;

		public void InvokeOnNextFrameUpdate(Action action)
		{
			if (action != null) m_frameInvokeStack.Push(action);
		}
		
		public void InvokeOnNextFixedUpdate(Action action)
		{
			if (action != null) m_fixedInvokeStack.Push(action);
		}

		public void AddFrameUpdate(IFrameUpdatable updatable) => m_frameUpdateCollection.Add(updatable);
		
		public void RemoveFrameUpdate(IFrameUpdatable updatable) => m_frameUpdateCollection.Remove(updatable);
		
		private void Update()
		{
			switch (m_gameState)
			{
				case GameState.Idle:
					break;
				case GameState.Preload:
					SettingsManager.Instance.Load();
					InputManager.Instance.ForcePlayerInput();
					m_gameState = GameState.Play;
					break;
				case GameState.Play:
					while (m_frameInvokeStack.TryPop(out var action)) action.Invoke();
					m_frameUpdateCollection.Update();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void AddFixedUpdate(IFixedUpdatable updatable) => m_fixedUpdateCollection.Add(updatable);
		
		public void RemoveFixedUpdate(IFixedUpdatable updatable) => m_fixedUpdateCollection.Remove(updatable);
		
		private void FixedUpdate()
		{
			if (m_gameState != GameState.Play) return;
			while (m_fixedInvokeStack.TryPop(out var action)) action.Invoke();
			m_fixedUpdateCollection.Update();
		}
	}
}