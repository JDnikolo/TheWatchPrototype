using Callbacks.Backing;
using Input;
using Managers.Persistent;
using UnityEngine;

namespace UI.Button
{
	[AddComponentMenu("UI/Elements/Back Button")]
	public sealed class BackButton : Elements.Button, IBackHook
	{
		public void OnBackPressed(InputState inputState)
		{
			if (inputState == InputState.Pressed) OnClick(-1);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			InputManager.Instance?.BackSpecial.AddHook(this);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			InputManager.Instance?.BackSpecial.RemoveHook(this);
		}
	}
}