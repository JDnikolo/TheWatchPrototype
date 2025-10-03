using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
	[AddComponentMenu("Managers/Game Manager")]
	public sealed class GameManager : Singleton<GameManager>
	{
		private readonly List<IStartable> m_startables = new();
		private readonly Stack<Action> m_invokeStack = new();
		
		private enum GameState : byte
		{
			SetupStart,
			Setup,
			Play,
		}

		private GameState m_gameState = GameState.SetupStart;
		
		internal void AddStartable(IStartable startable)
		{
			if (startable != null) m_startables.Add(startable);
		}

		public void InvokeOnNextUpdate(Action action)
		{
			if (action != null) m_invokeStack.Push(action);
		}

		private void Update()
		{
			switch (m_gameState)
			{
				case GameState.SetupStart:
					m_startables.Sort(SortByOrderReversed);
					m_gameState = GameState.Setup;
					break;
				case GameState.Setup:
					for (var i = m_startables.Count - 1; i >= 0; i--)
					{
						m_startables[i].OnStart();
						m_startables.RemoveAt(i);
					}
					
					m_gameState = GameState.Play;
					break;
				case GameState.Play:
					while (m_invokeStack.TryPop(out var action)) action.Invoke();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private int SortByOrderReversed(IStartable x, IStartable y) => y.StartOrder.CompareTo(x.StartOrder);
	}
}