using Attributes;
using Managers.Persistent;
using Runtime.Automation;
using Runtime.FrameUpdate;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Text
{
	[AddComponentMenu("UI/Text/Control Info Helper")]
	public sealed class ControlHelper : BaseBehaviour, IFrameUpdatable
#if UNITY_EDITOR
		, IBehaviourChecker
#endif
	{
		[SerializeField] private InputActionReference inputReference;
		[SerializeField] [DisableInInspector] private ControlText[] texts;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.Default;

		private bool m_enabled = true;
		
		public void OnFrameUpdate()
		{
			if (inputReference.action.WasPressedThisFrame()) SetEnabled(!m_enabled);
		}

		private void SetEnabled(bool enabled)
		{
			m_enabled = enabled;
			for (var i = 0; i < texts.Length; i++) texts[i].gameObject.SetActive(enabled);
		}

		private void Start() => GameManager.Instance?.AddFrameUpdate(this);

		private void OnDestroy() => GameManager.Instance?.RemoveFrameUpdate(this);

#if UNITY_EDITOR
		private BehaviorTester<ControlText> m_tester;

		public void OnCheckBehaviourStart() => m_tester.BeginTest();

		public void OnCheckBehaviour(MonoBehaviour monoBehaviour) =>
			m_tester.TestBehavior(monoBehaviour, ref texts);

		public void OnCheckBehaviourEnd()
		{
			if (!m_tester.EndTest(this, ref texts)) return;
			SetEnabled(true);
		}
#endif
	}
}