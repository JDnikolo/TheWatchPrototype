using UnityEngine;
using Variables.Enums;

namespace Variables
{
	[CreateAssetMenu(fileName = "Test", menuName = "Variables/Test")]
	public sealed class TestVariable : VariableObject<TestEnum>
	{
	}
}