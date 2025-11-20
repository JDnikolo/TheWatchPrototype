using UnityEngine;

#if UNITY_EDITOR
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