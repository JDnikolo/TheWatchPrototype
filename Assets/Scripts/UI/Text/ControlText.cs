using Attributes;
using Callbacks.Beforeplay;
using Input;
using Localization.Text;
using Managers.Persistent;
using UnityEngine;
using Utilities;

namespace UI.Text
{
	[AddComponentMenu("UI/Text/Control Info Text")]
	public sealed class ControlText : BaseBehaviour, IBeforePlay
	{
		[SerializeField] private GroupedControlEnum target;
		[SerializeField] private GroupedControlText text;

		[SerializeField] [AutoAssigned(AssignMode.Self | AssignMode.Forced, typeof(TextWriter))]
		private TextWriter textWriter;
		
		public void OnBeforePlay()
		{
			var inputManager = InputManager.Instance;
			var action = inputManager.GetAction(target);
			var inputBindings = inputManager.GetBindingIndex(target, ref action);
			var prefix = $"{text.Values[(int) target].Text}";
			if (inputBindings.Item2 < 0)
				textWriter.WriteText(
					$"{prefix} with {action.ToBindingDisplayString(inputBindings.Item1)}");
			else
				textWriter.WriteText(
					$"{prefix} with {action.ToBindingDisplayString(inputBindings.Item1)} or {action.ToBindingDisplayString(inputBindings.Item2)}");
		}
#if UNITY_EDITOR
		private void OnValidate() => this.UpdateNameTo(target.ToString());
#endif
	}
}