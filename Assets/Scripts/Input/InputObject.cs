using Attributes;
using UnityEngine;

namespace Input
{
	[CreateAssetMenu(fileName = "Input", menuName = "Input/Single Input")]
	public sealed class InputObject : InputBase
	{
		[MinCount((int) ControlSchemeEnum.ENUM_LENGTH)] [SerializeField, EnumArray(typeof(ControlSchemeEnum))]
		private ControlSchemeObject[] controlSchemes;
		[SerializeField] [DisableInInspector] private CompoundInputObject compoundInput;
	}
}