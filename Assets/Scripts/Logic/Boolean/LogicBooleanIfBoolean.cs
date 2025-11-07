using UnityEngine;

namespace Logic.Boolean
{
	[CreateAssetMenu(fileName = "IfBoolean", menuName = "Logic/Boolean/Boolean IF-Gate")]
	public sealed class LogicBooleanIfBoolean : LogicBooleanIf<bool>
	{
		protected override bool Equals(bool x, bool y) => x == y;
	}
}