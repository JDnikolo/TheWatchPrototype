using System;
using System.Collections.Generic;
using Managers.Persistent;
using Runtime;

namespace Boxing
{
	public sealed class Delayer
	{
		private const int Capacity = 20;
		
		private static class Generator
		{
			private static readonly Stack<Delayer> m_stack = new(Capacity);

			public static Delayer Request(Action action, int cycles)
			{
				if (m_stack.Count == 0) return new Delayer(action, cycles);
				var result = m_stack.Pop();
				result.m_action = action;
				result.m_cycles = cycles;
				return result;
			}

			public static void Return(Delayer value)
			{
				if (m_stack.Count >= Capacity) return;
				m_stack.Push(value);
			}
		}

		public static void DelayedExecution(Action action, int cycles, UpdateEnum update) => 
			Generator.Request(action,cycles).Delay(update);

		private Action m_action;
		private UpdateEnum m_update;
		private int m_cycles;
		
		private Delayer(Action action, int cycles)
		{
			m_action = action;
			m_cycles = cycles;
		}

		private void Delay(UpdateEnum update)
		{
			m_update = update;
			switch (m_update)
			{
				case UpdateEnum.FrameUpdate:
					GameManager.Instance.InvokeOnNextFrameUpdate(TryFire);
					break;
				case UpdateEnum.LateUpdate:
					GameManager.Instance.InvokeOnNextLateUpdate(TryFire);
					break;
				case UpdateEnum.FixedUpdate:
					GameManager.Instance.InvokeOnNextFixedUpdate(TryFire);
					break;
			}
		}

		private void TryFire()
		{
			if (--m_cycles > 0) Delay(m_update);
			else
			{
				m_action?.Invoke();
				m_action = null;
				m_update = default;
				m_cycles = 0;
				Generator.Return(this);
			}
		}
	}
}