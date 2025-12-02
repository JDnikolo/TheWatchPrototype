using Attributes;
using Managers.Persistent;
using Runtime.Automation;
using Runtime.FrameUpdate;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Text
{
	[AddComponentMenu("UI/Text/Control Info Helper")]
	public sealed class ControlHelper : MonoBehaviour, IFrameUpdatable
#if UNITY_EDITOR
		, IBehaviourChecker
#endif
	{
		[SerializeField] private string showControlsActionName = "ShowControls";
		[SerializeField] [DisableInInspector] private ControlText[] texts;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.Default;

		private InputAction m_showControlsAction;
		private bool m_enabled = true;
		
		public void OnFrameUpdate()
		{
			m_showControlsAction ??= InputManager.Instance.PlayerMap.GetAction(showControlsActionName);
			if (m_showControlsAction.WasPressedThisFrame()) SetEnabled(!m_enabled);
		}

		private void SetEnabled(bool enabled)
		{
			m_enabled = enabled;
			for (var i = 0; i < texts.Length; i++) texts[i].enabled = m_enabled;
		}
		
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