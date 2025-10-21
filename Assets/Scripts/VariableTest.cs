using Interactables;
using UnityEngine;
using Variables;
using Variables.Enums;

public class VariableTest : Interactable
{
	[SerializeField] private TestVariable variable;
	[SerializeField] private TestEnum targetValue;
	
	public override void Interact() => variable.Value = targetValue;
}