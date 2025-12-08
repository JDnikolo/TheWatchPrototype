using System;
using System.Collections.Generic;
using Runtime.Automation;
using Utilities;

namespace Boxing
{
	public sealed class ImmutableRef<T> : IRef<T>, IEquatable<ImmutableRef<T>> 
#if UNITY_EDITOR
		, IEditorDisplayable
#endif
		where T : struct
	{
		private const int Capacity = 20;
		
		private static class Generator
		{
			private static readonly Stack<ImmutableRef<T>> m_stack = new(Capacity);

			public static ImmutableRef<T> Request(T value)
			{
				if (m_stack.Count == 0) return new ImmutableRef<T>(value);
				var result = m_stack.Pop();
				result.m_value = value;
				return result;
			}

			public static void Return(ImmutableRef<T> value)
			{
				if (m_stack.Count >= Capacity) return;
				m_stack.Push(value);
			}
		}

		private T m_value;
		
		public T Value => m_value;

		private ImmutableRef(T value) => m_value = value;

		public T GetValue() => m_value;
		
		public void Return() => Generator.Return(this);
		
		public bool Equals(ImmutableRef<T> other)
		{
			if (other is null) return false;
			if (ReferenceEquals(this, other)) return true;
			return EqualityComparer<T>.Default.Equals(m_value, other.m_value);
		}

		public override bool Equals(object obj) => obj is ImmutableRef<T> other && Equals(other);

		public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(m_value);

		public override string ToString() => $"[ImmutableRef] {m_value}";

		public static implicit operator T(ImmutableRef<T> reference) => reference.m_value;

		public static implicit operator ImmutableRef<T>(T value) => Generator.Request(value);
#if UNITY_EDITOR
		public void DisplayInEditor() => m_value.Display(null);

		public void DisplayInEditor(string name) => m_value.Display(name);
#endif
	}
}