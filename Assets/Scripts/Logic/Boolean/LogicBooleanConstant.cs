using UnityEngine;

namespace Logic.Boolean
{
	[CreateAssetMenu(fileName = "Constant", menuName = "Logic/Boolean/Constant")]
	public sealed class LogicBooleanConstant : LogicBoolean
	{
		[SerializeField] private bool constantValue;
		
		public override bool Evaluate() => constantValue;
	}
}