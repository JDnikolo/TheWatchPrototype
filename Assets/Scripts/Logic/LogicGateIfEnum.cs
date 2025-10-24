using UnityEngine;
using System;
using Variables.Enums;

namespace Logic
{
	[CreateAssetMenu(fileName = "IfEnum", menuName = "Logic/Enum/IF-Gate")]
	public sealed class LogicGateIfEnum : LogicGateIf<VictorIntroStateEnum>
	{
		protected override bool Equals(VictorIntroStateEnum x, VictorIntroStateEnum y) => x == y;
	}
}