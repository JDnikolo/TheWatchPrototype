using Interactables;
using UnityEngine;
using Variables;
using Variables.Enums;

namespace Tests
{
	public sealed class VariableTest : Interactable
	{
		[SerializeField] private TestVariable variable;
		[SerializeField] private TestEnum targetValue;
	
		public override void Interact() => variable.Value = targetValue;
	}
}