using UnityEngine;

namespace Logic
{
	public abstract class LogicGate : ScriptableObject
	{
		public abstract bool Evaluate();
	}
}