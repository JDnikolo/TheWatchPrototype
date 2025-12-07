#if UNITY_EDITOR
using UnityEngine;

namespace Runtime.Automation
{
	public interface IBehaviourChecker
	{
		void OnCheckBehaviourStart();
		void OnCheckBehaviour(MonoBehaviour monoBehaviour);
		void OnCheckBehaviourEnd();
	}
}
#endif