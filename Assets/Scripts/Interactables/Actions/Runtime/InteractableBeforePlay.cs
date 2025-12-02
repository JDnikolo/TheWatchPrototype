using Attributes;
using Callbacks.Beforeplay;
using Managers.Persistent;
using Runtime;
using Runtime.Automation;
using UnityEngine;

namespace Interactables.Actions.Runtime
{
	[AddComponentMenu("Interactables/Runtime/Register Before Play")]
	public sealed class InteractableBeforePlay : Interactable
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
					if (behaviours[i] is IBeforePlay beforePlay)
						gameManager.RegisterBeforePlay(beforePlay);
		}
#if UNITY_EDITOR
		private BehaviorTester m_tester;

		public void OnCheckBehaviourStart() => m_tester.BeginTest();

		public void OnCheckBehaviour(MonoBehaviour monoBehaviour) =>
			m_tester.TestBehavior<IBeforePlay>(monoBehaviour, ref behaviours);

		public void OnCheckBehaviourEnd() => m_tester.EndTest(this, ref behaviours);
#endif
	}
}