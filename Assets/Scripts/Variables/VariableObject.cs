using UnityEngine;

namespace Variables
{
	public abstract class VariableObject : BaseObject
	{
		public abstract void ResetValue();
		
		private void OnEnable() => ResetValue();
	}
	
	public abstract class VariableObject<T> : VariableObject
	{
		[SerializeField] private T startingValue;

		public T Value { get; set; }

		public sealed override void ResetValue() => Value = startingValue;
	}
}