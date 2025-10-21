using UnityEngine;

namespace Variables
{
	public abstract class VariableObject<T> : ScriptableObject
	{
		[SerializeField] private T startingValue;

		private void OnEnable() => Value = startingValue;

		public T Value { get; set; }
	}
}