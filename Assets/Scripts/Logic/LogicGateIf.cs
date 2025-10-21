using UnityEngine;
using Variables;

namespace Logic
{
	public abstract class LogicGateIf<T> : LogicGate
	{
		[SerializeField] private VariableObject<T> variable;
		[SerializeField] private T targetValue;

		public sealed override bool Evaluate() => Equals(variable.Value, targetValue);

		protected abstract bool Equals(T x, T y);
	}
}