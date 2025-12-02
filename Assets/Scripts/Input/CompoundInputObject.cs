using UnityEngine;

namespace Input
{
	[CreateAssetMenu(fileName = "CompoundInput", menuName = "Input/Compound Input")]
	public sealed class CompoundInputObject : InputBase
	{
		[SerializeField] private InputObject[] inputs;
	}
}