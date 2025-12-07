using Attributes;
using UnityEngine;
using Variables;

namespace Logic.Boolean
{
	public abstract class LogicBooleanIf<T> : LogicBoolean
	{
		[SerializeField] private VariableObject<T> variable;
		[CanBeNull] [SerializeField] private T targetValue;

		public sealed override bool Evaluate() => Equals(variable.Value, targetValue);

		protected abstract bool Equals(T x, T y);
	}
}