using Input;
using Managers.Persistent;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tests
{
	public sealed class InputTest : BaseBehaviour
	{
		[SerializeField] private InputActionReference actionReference;
		[SerializeField] private ControlSchemeEnum scheme;
		[SerializeField] private int bindingOverride;
		[SerializeField] private InputBinding.DisplayStringOptions options;

		private void Start()
		{
			var action = actionReference.action;
			var bindings = action.bindings;
			var groupMask = InputManager.GetGroup(ControlSchemeEnum.Keyboard);
			var mask = InputBinding.MaskByGroup(groupMask);
			for (var i = 0; i < bindings.Count; i++)
			{
				var binding = bindings[i];
				if (mask.Matches(binding))
				{
					Debug.Log(binding.path);
				}
			}
		}
	}
}