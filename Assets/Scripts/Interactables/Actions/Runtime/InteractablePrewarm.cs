using Attributes;
using Callbacks.Prewarm;
using Managers.Persistent;
using Runtime.Automation;
using UnityEngine;

namespace Interactables.Actions.Runtime
{
	[AddComponentMenu("Interactables/Runtime/Invoke Prewarm")]
	public sealed class InteractablePrewarm : Interactable
#if UNITY_EDITOR
		, IBehaviourChecker
#endif
	{
		[SerializeField] [DisableInInspector] private MonoBehaviour[] behaviours;

		public override void Interact()
		{
			var gameManager = GameManager.Instance;
			if (gameManager)
				for (var i = 0; i < behaviours.Length; i++)
					if (behaviours[i] is IPrewarm prewarm)
						prewarm.OnPrewarm();
		}
		
#if UNITY_EDITOR
		private BehaviorTester m_tester;
		
		public void OnCheckBehaviourStart() => m_tester.BeginTest();

		public void OnCheckBehaviour(MonoBehaviour monoBehaviour) =>
			m_tester.TestBehavior<IPrewarm>(monoBehaviour, ref behaviours);

		public void OnCheckBehaviourEnd() => m_tester.EndTest(this, ref behaviours);
#endif
	}
}