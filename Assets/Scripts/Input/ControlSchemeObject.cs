using Attributes;
using UnityEngine;

namespace Input
{
	[CreateAssetMenu(fileName = "ControlScheme", menuName = "Input/Control Scheme")]
	public sealed class ControlSchemeObject : BaseObject
	{
		[SerializeField] private int[] bindingIndexes;
		[SerializeField] [DisableInInspector] private InputObject input;
	}
}