using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
	public sealed class CloseTextWriter : TextWriterFinished
	{
		[SerializeField] private string closeActionName = "SkipDialogue";
		
		private InputAction m_closeAction;
		
		public override void OnTextWriterFinished() => enabled = true;

		private void Start() => m_closeAction = InputManager.Instance.GetUIAction(closeActionName);

		private void Update()
		{
			if (!m_closeAction.WasPressedThisFrame()) return;
			enabled = false;
			UIManager.Instance.CloseDialogue();
			InputManager.Instance.ForcePlayerInput();
		}
	}
}