using Attributes;
using Callbacks.Beforeplay;
using Localization.Text;
using Managers.Persistent;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace UI.Text
{
	[AddComponentMenu("UI/Text/Control Info Text")]
	public sealed class ControlText : BaseBehaviour, IBeforePlay
	{
		[CanBeNullInPrefab, SerializeField] private InputActionReference target;
		[CanBeNullInPrefab, SerializeField] private TextObject text;

		[SerializeField] [AutoAssigned(AssignModeFlags.Self, typeof(TextWriter))]
		private TextWriter textWriter;
		
		public void OnBeforePlay()
		{
			var action = target.action;
			var inputBindings = InputManager.Instance.GetBindingIndexes(action);
			if (inputBindings.HasSecondary)
				textWriter.WriteText(
					$"{text.Text}: {action.ToBindingDisplayString(inputBindings.Primary)}/{action.ToBindingDisplayString(inputBindings.Secondary)}");
			else
				textWriter.WriteText(
					$"{text.Text}: {action.ToBindingDisplayString(inputBindings.Primary)}");
		}
#if UNITY_EDITOR
		private void OnValidate()
		{
			if (target) this.UpdateNameTo(target.ToString());
		}
#endif
	}
}