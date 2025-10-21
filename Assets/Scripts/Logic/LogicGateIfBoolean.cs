using UnityEngine;

namespace Logic
{
	[CreateAssetMenu(fileName = "IfBoolean", menuName = "Logic/Boolean/IF-Gate")]
	public sealed class LogicGateIfBoolean : LogicGateIf<bool>
	{
		protected override bool Equals(bool x, bool y) => x == y;
	}
}