using System;
using System.Collections.Generic;
using Runtime.Automation;
using Utilities;

namespace Boxing
{
	public sealed class MutableRef<T> : IRef<T>, IEquatable<MutableRef<T>> 
#if UNITY_EDITOR
		, IEditorDisplayable
#endif
		where T : struct
	{
		private const int Capacity = 20;
		
		private static class Generator
		{
			private static readonly Stack<MutableRef<T>> m_stack = new(Capacity);

			public static MutableRef<T> Request(T value)
			{
				if (m_stack.Count == 0) return new MutableRef<T> {Value = value};
				return m_stack.Pop();
			}

			public static void Return(MutableRef<T> value)
			{
				if (m_stack.Count >= Capacity) return;
				m_stack.Push(value);
			}
		}
		
		public T Value;

		public T GetValue() => Value;
		
		public void Return() => Generator.Return(this);
		
		public bool Equals(MutableRef<T> other)
		{
			if (other is null) return false;
			if (ReferenceEquals(this, other)) return true;
			return EqualityComparer<T>.Default.Equals(Value, other.Value);
		}

		public override bool Equals(object obj) => obj is MutableRef<T> other && Equals(other);

		public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Value);

		public override string ToString() => $"[MutableRef] {Value}";
		
		public static implicit operator T(MutableRef<T> reference) => reference.Value;

		public static implicit operator MutableRef<T>(T value) => Generator.Request(value);
#if UNITY_EDITOR
		public void DisplayInEditor() => Value.Display(null);

		public void DisplayInEditor(string name) => Value.Display(name);
#endif
	}
}