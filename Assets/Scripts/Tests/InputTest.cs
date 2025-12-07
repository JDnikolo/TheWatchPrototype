using UnityEngine;
using UnityEngine.InputSystem;

namespace Tests
{
	public sealed class InputTest : BaseBehaviour
	{
		[SerializeField] private InputActionReference actionReference;
		[SerializeField] private string bindingId;
		[SerializeField] private InputBinding.DisplayStringOptions options;

		private void Start()
		{
			var bindings = actionReference.action.bindings;
			var mask = actionReference.action.bindingMask;
			if (!mask.HasValue) Debug.Log("Mask is null");
			else Debug.Log(mask.Value.ToDisplayString(options));
			for (var i = 0; i < bindings.Count; i++)
			{
				var binding = bindings[i];
				Debug.Log(binding.ToDisplayString(options));
				//Debug.Log(binding.);
			}
		}
	}
}