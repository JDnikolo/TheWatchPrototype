using UnityEngine;
using Variables.Enums;

namespace Logic.Boolean
{
	[CreateAssetMenu(fileName = "IfEnum", menuName = "Logic/Boolean/Enum IF-Gate")]
	public sealed class LogicBooleanIfEnum : LogicBooleanIf<VictorIntroStateEnum>
	{
		protected override bool Equals(VictorIntroStateEnum x, VictorIntroStateEnum y) => x == y;
	}
}